
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


namespace Business.Handlers.RestaurantImages.Commands
{
    /// <summary>
    /// 
    /// </summary>
    public class DeleteRestaurantImageCommand : IRequest<IResult>
    {
        public int Id { get; set; }

        public class DeleteRestaurantImageCommandHandler : IRequestHandler<DeleteRestaurantImageCommand, IResult>
        {
            private readonly IRestaurantImageRepository _restaurantImageRepository;
            private readonly IMediator _mediator;

            public DeleteRestaurantImageCommandHandler(IRestaurantImageRepository restaurantImageRepository, IMediator mediator)
            {
                _restaurantImageRepository = restaurantImageRepository;
                _mediator = mediator;
            }

            [CacheRemoveAspect("Get")]
            [LogAspect(typeof(FileLogger))]
            [SecuredOperation(Priority = 1)]
            public async Task<IResult> Handle(DeleteRestaurantImageCommand request, CancellationToken cancellationToken)
            {
                var restaurantImageToDelete = _restaurantImageRepository.Get(p => p.Id == request.Id);

                _restaurantImageRepository.Delete(restaurantImageToDelete);
                await _restaurantImageRepository.SaveChangesAsync();
                return new SuccessResult(Messages.Deleted);
            }
        }
    }
}

