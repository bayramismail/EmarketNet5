
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

namespace Business.Handlers.Restaurants.Queries
{

    public class GetRestaurantsQuery : IRequest<IDataResult<IEnumerable<Restaurant>>>
    {
        public class GetRestaurantsQueryHandler : IRequestHandler<GetRestaurantsQuery, IDataResult<IEnumerable<Restaurant>>>
        {
            private readonly IRestaurantRepository _restaurantRepository;
            private readonly IMediator _mediator;

            public GetRestaurantsQueryHandler(IRestaurantRepository restaurantRepository, IMediator mediator)
            {
                _restaurantRepository = restaurantRepository;
                _mediator = mediator;
            }

            [PerformanceAspect(5)]
            [CacheAspect(10)]
            [LogAspect(typeof(FileLogger))]
            [SecuredOperation(Priority = 1)]
            public async Task<IDataResult<IEnumerable<Restaurant>>> Handle(GetRestaurantsQuery request, CancellationToken cancellationToken)
            {
                return new SuccessDataResult<IEnumerable<Restaurant>>(await _restaurantRepository.GetListAsync());
            }
        }
    }
}