
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


namespace Business.Handlers.Restaurants.Commands
{
    /// <summary>
    /// 
    /// </summary>
    public class DeleteRestaurantCommand : IRequest<IResult>
    {
        public int Id { get; set; }

        public class DeleteRestaurantCommandHandler : IRequestHandler<DeleteRestaurantCommand, IResult>
        {
            private readonly IRestaurantRepository _restaurantRepository;
            private readonly IMediator _mediator;

            public DeleteRestaurantCommandHandler(IRestaurantRepository restaurantRepository, IMediator mediator)
            {
                _restaurantRepository = restaurantRepository;
                _mediator = mediator;
            }

            [CacheRemoveAspect("Get")]
            [LogAspect(typeof(FileLogger))]
            [SecuredOperation(Priority = 1)]
            public async Task<IResult> Handle(DeleteRestaurantCommand request, CancellationToken cancellationToken)
            {
                var restaurantToDelete = _restaurantRepository.Get(p => p.Id == request.Id);

                _restaurantRepository.Delete(restaurantToDelete);
                await _restaurantRepository.SaveChangesAsync();
                return new SuccessResult(Messages.Deleted);
            }
        }
    }
}

