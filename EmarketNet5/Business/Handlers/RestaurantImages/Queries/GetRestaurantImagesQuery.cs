
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

namespace Business.Handlers.RestaurantImages.Queries
{

    public class GetRestaurantImagesQuery : IRequest<IDataResult<IEnumerable<RestaurantImage>>>
    {
        public class GetRestaurantImagesQueryHandler : IRequestHandler<GetRestaurantImagesQuery, IDataResult<IEnumerable<RestaurantImage>>>
        {
            private readonly IRestaurantImageRepository _restaurantImageRepository;
            private readonly IMediator _mediator;

            public GetRestaurantImagesQueryHandler(IRestaurantImageRepository restaurantImageRepository, IMediator mediator)
            {
                _restaurantImageRepository = restaurantImageRepository;
                _mediator = mediator;
            }

            [PerformanceAspect(5)]
            [CacheAspect(10)]
            [LogAspect(typeof(FileLogger))]
            [SecuredOperation(Priority = 1)]
            public async Task<IDataResult<IEnumerable<RestaurantImage>>> Handle(GetRestaurantImagesQuery request, CancellationToken cancellationToken)
            {
                return new SuccessDataResult<IEnumerable<RestaurantImage>>(await _restaurantImageRepository.GetListAsync());
            }
        }
    }
}