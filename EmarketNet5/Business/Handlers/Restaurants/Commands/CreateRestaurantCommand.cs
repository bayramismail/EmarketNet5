
using Business.BusinessAspects;
using Business.Constants;
using Core.Aspects.Autofac.Caching;
using Core.Aspects.Autofac.Logging;
using Core.Aspects.Autofac.Validation;
using Core.CrossCuttingConcerns.Logging.Serilog.Loggers;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.Concrete;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using System.Linq;
using Business.Handlers.Restaurants.ValidationRules;

namespace Business.Handlers.Restaurants.Commands
{
    /// <summary>
    /// 
    /// </summary>
    public class CreateRestaurantCommand : IRequest<IResult>
    {

        public int UserId { get; set; }
        public string Name { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public string Detail { get; set; }
        public System.DateTime CreateDate { get; set; }
        public bool Active { get; set; }


        public class CreateRestaurantCommandHandler : IRequestHandler<CreateRestaurantCommand, IResult>
        {
            private readonly IRestaurantRepository _restaurantRepository;
            private readonly IMediator _mediator;
            public CreateRestaurantCommandHandler(IRestaurantRepository restaurantRepository, IMediator mediator)
            {
                _restaurantRepository = restaurantRepository;
                _mediator = mediator;
            }

            [ValidationAspect(typeof(CreateRestaurantValidator), Priority = 1)]
            [CacheRemoveAspect("Get")]
            [LogAspect(typeof(FileLogger))]
            [SecuredOperation(Priority = 1)]
            public async Task<IResult> Handle(CreateRestaurantCommand request, CancellationToken cancellationToken)
            {
                var isThereRestaurantRecord = _restaurantRepository.Query().Any(u => u.UserId == request.UserId);

                if (isThereRestaurantRecord == true)
                    return new ErrorResult(Messages.NameAlreadyExist);

                var addedRestaurant = new Restaurant
                {
                    UserId = request.UserId,
                    Name = request.Name,
                    Phone = request.Phone,
                    Email = request.Email,
                    Detail = request.Detail,
                    CreateDate = request.CreateDate,
                    Active = request.Active,

                };

                _restaurantRepository.Add(addedRestaurant);
                await _restaurantRepository.SaveChangesAsync();
                return new SuccessResult(Messages.Added);
            }
        }
    }
}