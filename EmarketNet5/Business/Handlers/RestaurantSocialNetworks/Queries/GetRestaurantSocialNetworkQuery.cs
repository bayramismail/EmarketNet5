
using Business.BusinessAspects;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.Concrete;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using Core.Aspects.Autofac.Logging;
using Core.CrossCuttingConcerns.Logging.Serilog.Loggers;


namespace Business.Handlers.RestaurantSocialNetworks.Queries
{
    public class GetRestaurantSocialNetworkQuery : IRequest<IDataResult<RestaurantSocialNetwork>>
    {
        public int Id { get; set; }

        public class GetRestaurantSocialNetworkQueryHandler : IRequestHandler<GetRestaurantSocialNetworkQuery, IDataResult<RestaurantSocialNetwork>>
        {
            private readonly IRestaurantSocialNetworkRepository _restaurantSocialNetworkRepository;
            private readonly IMediator _mediator;

            public GetRestaurantSocialNetworkQueryHandler(IRestaurantSocialNetworkRepository restaurantSocialNetworkRepository, IMediator mediator)
            {
                _restaurantSocialNetworkRepository = restaurantSocialNetworkRepository;
                _mediator = mediator;
            }
            [LogAspect(typeof(FileLogger))]
            [SecuredOperation(Priority = 1)]
            public async Task<IDataResult<RestaurantSocialNetwork>> Handle(GetRestaurantSocialNetworkQuery request, CancellationToken cancellationToken)
            {
                var restaurantSocialNetwork = await _restaurantSocialNetworkRepository.GetAsync(p => p.Id == request.Id);
                return new SuccessDataResult<RestaurantSocialNetwork>(restaurantSocialNetwork);
            }
        }
    }
}
