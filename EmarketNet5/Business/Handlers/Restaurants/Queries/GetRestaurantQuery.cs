
using Business.BusinessAspects;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.Concrete;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using Core.Aspects.Autofac.Logging;
using Core.CrossCuttingConcerns.Logging.Serilog.Loggers;


namespace Business.Handlers.Restaurants.Queries
{
    public class GetRestaurantQuery : IRequest<IDataResult<Restaurant>>
    {
        public int Id { get; set; }

        public class GetRestaurantQueryHandler : IRequestHandler<GetRestaurantQuery, IDataResult<Restaurant>>
        {
            private readonly IRestaurantRepository _restaurantRepository;
            private readonly IMediator _mediator;

            public GetRestaurantQueryHandler(IRestaurantRepository restaurantRepository, IMediator mediator)
            {
                _restaurantRepository = restaurantRepository;
                _mediator = mediator;
            }
            [LogAspect(typeof(FileLogger))]
            [SecuredOperation(Priority = 1)]
            public async Task<IDataResult<Restaurant>> Handle(GetRestaurantQuery request, CancellationToken cancellationToken)
            {
                var restaurant = await _restaurantRepository.GetAsync(p => p.Id == request.Id);
                return new SuccessDataResult<Restaurant>(restaurant);
            }
        }
    }
}
