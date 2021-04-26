
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

namespace Business.Handlers.RestaurantAddresses.Queries
{

    public class GetRestaurantAddressesQuery : IRequest<IDataResult<IEnumerable<RestaurantAddress>>>
    {
        public class GetRestaurantAddressesQueryHandler : IRequestHandler<GetRestaurantAddressesQuery, IDataResult<IEnumerable<RestaurantAddress>>>
        {
            private readonly IRestaurantAddressRepository _restaurantAddressRepository;
            private readonly IMediator _mediator;

            public GetRestaurantAddressesQueryHandler(IRestaurantAddressRepository restaurantAddressRepository, IMediator mediator)
            {
                _restaurantAddressRepository = restaurantAddressRepository;
                _mediator = mediator;
            }

            [PerformanceAspect(5)]
            [CacheAspect(10)]
            [LogAspect(typeof(FileLogger))]
            [SecuredOperation(Priority = 1)]
            public async Task<IDataResult<IEnumerable<RestaurantAddress>>> Handle(GetRestaurantAddressesQuery request, CancellationToken cancellationToken)
            {
                return new SuccessDataResult<IEnumerable<RestaurantAddress>>(await _restaurantAddressRepository.GetListAsync());
            }
        }
    }
}