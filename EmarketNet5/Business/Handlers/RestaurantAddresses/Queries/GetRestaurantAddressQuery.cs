
using Business.BusinessAspects;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.Concrete;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using Core.Aspects.Autofac.Logging;
using Core.CrossCuttingConcerns.Logging.Serilog.Loggers;


namespace Business.Handlers.RestaurantAddresses.Queries
{
    public class GetRestaurantAddressQuery : IRequest<IDataResult<RestaurantAddress>>
    {
        public int Id { get; set; }

        public class GetRestaurantAddressQueryHandler : IRequestHandler<GetRestaurantAddressQuery, IDataResult<RestaurantAddress>>
        {
            private readonly IRestaurantAddressRepository _restaurantAddressRepository;
            private readonly IMediator _mediator;

            public GetRestaurantAddressQueryHandler(IRestaurantAddressRepository restaurantAddressRepository, IMediator mediator)
            {
                _restaurantAddressRepository = restaurantAddressRepository;
                _mediator = mediator;
            }
            [LogAspect(typeof(FileLogger))]
            [SecuredOperation(Priority = 1)]
            public async Task<IDataResult<RestaurantAddress>> Handle(GetRestaurantAddressQuery request, CancellationToken cancellationToken)
            {
                var restaurantAddress = await _restaurantAddressRepository.GetAsync(p => p.Id == request.Id);
                return new SuccessDataResult<RestaurantAddress>(restaurantAddress);
            }
        }
    }
}
