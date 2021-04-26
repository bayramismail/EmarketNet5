
using Business.BusinessAspects;
using Core.Aspects.Autofac.Performance;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.Concrete;
using MediatR;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Core.Aspects.Autofac.Logging;
using Core.CrossCuttingConcerns.Logging.Serilog.Loggers;
using Core.Aspects.Autofac.Caching;

namespace Business.Handlers.RestaurantSocialNetworks.Queries
{

    public class GetRestaurantSocialNetworksQuery : IRequest<IDataResult<IEnumerable<RestaurantSocialNetwork>>>
    {
        public class GetRestaurantSocialNetworksQueryHandler : IRequestHandler<GetRestaurantSocialNetworksQuery, IDataResult<IEnumerable<RestaurantSocialNetwork>>>
        {
            private readonly IRestaurantSocialNetworkRepository _restaurantSocialNetworkRepository;
            private readonly IMediator _mediator;

            public GetRestaurantSocialNetworksQueryHandler(IRestaurantSocialNetworkRepository restaurantSocialNetworkRepository, IMediator mediator)
            {
                _restaurantSocialNetworkRepository = restaurantSocialNetworkRepository;
                _mediator = mediator;
            }

            [PerformanceAspect(5)]
            [CacheAspect(10)]
            [LogAspect(typeof(FileLogger))]
            [SecuredOperation(Priority = 1)]
            public async Task<IDataResult<IEnumerable<RestaurantSocialNetwork>>> Handle(GetRestaurantSocialNetworksQuery request, CancellationToken cancellationToken)
            {
                return new SuccessDataResult<IEnumerable<RestaurantSocialNetwork>>(await _restaurantSocialNetworkRepository.GetListAsync());
            }
        }
    }
}