
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


namespace Business.Handlers.RestaurantSocialNetworks.Commands
{
    /// <summary>
    /// 
    /// </summary>
    public class DeleteRestaurantSocialNetworkCommand : IRequest<IResult>
    {
        public int Id { get; set; }

        public class DeleteRestaurantSocialNetworkCommandHandler : IRequestHandler<DeleteRestaurantSocialNetworkCommand, IResult>
        {
            private readonly IRestaurantSocialNetworkRepository _restaurantSocialNetworkRepository;
            private readonly IMediator _mediator;

            public DeleteRestaurantSocialNetworkCommandHandler(IRestaurantSocialNetworkRepository restaurantSocialNetworkRepository, IMediator mediator)
            {
                _restaurantSocialNetworkRepository = restaurantSocialNetworkRepository;
                _mediator = mediator;
            }

            [CacheRemoveAspect("Get")]
            [LogAspect(typeof(FileLogger))]
            [SecuredOperation(Priority = 1)]
            public async Task<IResult> Handle(DeleteRestaurantSocialNetworkCommand request, CancellationToken cancellationToken)
            {
                var restaurantSocialNetworkToDelete = _restaurantSocialNetworkRepository.Get(p => p.Id == request.Id);

                _restaurantSocialNetworkRepository.Delete(restaurantSocialNetworkToDelete);
                await _restaurantSocialNetworkRepository.SaveChangesAsync();
                return new SuccessResult(Messages.Deleted);
            }
        }
    }
}

