
using Business.Constants;
using Core.Aspects.Autofac.Caching;
using Business.BusinessAspects;
using Core.Aspects.Autofac.Logging;
using Core.CrossCuttingConcerns.Logging.Serilog.Loggers;
using Core.Utilities.Results;
using DataAccess.Abstract;
using MediatR;
using System.Threading;
using System.Threading.Tasks;


namespace Business.Handlers.RestaurantAddresses.Commands
{
    /// <summary>
    /// 
    /// </summary>
    public class DeleteRestaurantAddressCommand : IRequest<IResult>
    {
        public int Id { get; set; }

        public class DeleteRestaurantAddressCommandHandler : IRequestHandler<DeleteRestaurantAddressCommand, IResult>
        {
            private readonly IRestaurantAddressRepository _restaurantAddressRepository;
            private readonly IMediator _mediator;

            public DeleteRestaurantAddressCommandHandler(IRestaurantAddressRepository restaurantAddressRepository, IMediator mediator)
            {
                _restaurantAddressRepository = restaurantAddressRepository;
                _mediator = mediator;
            }

            [CacheRemoveAspect("Get")]
            [LogAspect(typeof(FileLogger))]
            [SecuredOperation(Priority = 1)]
            public async Task<IResult> Handle(DeleteRestaurantAddressCommand request, CancellationToken cancellationToken)
            {
                var restaurantAddressToDelete = _restaurantAddressRepository.Get(p => p.Id == request.Id);

                _restaurantAddressRepository.Delete(restaurantAddressToDelete);
                await _restaurantAddressRepository.SaveChangesAsync();
                return new SuccessResult(Messages.Deleted);
            }
        }
    }
}

