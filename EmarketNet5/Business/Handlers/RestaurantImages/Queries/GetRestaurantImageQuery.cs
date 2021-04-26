
using Business.BusinessAspects;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.Concrete;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using Core.Aspects.Autofac.Logging;
using Core.CrossCuttingConcerns.Logging.Serilog.Loggers;


namespace Business.Handlers.RestaurantImages.Queries
{
    public class GetRestaurantImageQuery : IRequest<IDataResult<RestaurantImage>>
    {
        public int Id { get; set; }

        public class GetRestaurantImageQueryHandler : IRequestHandler<GetRestaurantImageQuery, IDataResult<RestaurantImage>>
        {
            private readonly IRestaurantImageRepository _restaurantImageRepository;
            private readonly IMediator _mediator;

            public GetRestaurantImageQueryHandler(IRestaurantImageRepository restaurantImageRepository, IMediator mediator)
            {
                _restaurantImageRepository = restaurantImageRepository;
                _mediator = mediator;
            }
            [LogAspect(typeof(FileLogger))]
            [SecuredOperation(Priority = 1)]
            public async Task<IDataResult<RestaurantImage>> Handle(GetRestaurantImageQuery request, CancellationToken cancellationToken)
            {
                var restaurantImage = await _restaurantImageRepository.GetAsync(p => p.Id == request.Id);
                return new SuccessDataResult<RestaurantImage>(restaurantImage);
            }
        }
    }
}
