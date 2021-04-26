
using Business.Constants;
using Business.BusinessAspects;
using Core.Aspects.Autofac.Caching;
using Core.Aspects.Autofac.Logging;
using Core.CrossCuttingConcerns.Logging.Serilog.Loggers;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.Concrete;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using System.Linq;
using Core.Aspects.Autofac.Validation;
using Business.Handlers.Restaurants.ValidationRules;


namespace Business.Handlers.Restaurants.Commands
{


    public class UpdateRestaurantCommand : IRequest<IResult>
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public string Name { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public string Detail { get; set; }
        public System.DateTime CreateDate { get; set; }
        public bool Active { get; set; }

        public class UpdateRestaurantCommandHandler : IRequestHandler<UpdateRestaurantCommand, IResult>
        {
            private readonly IRestaurantRepository _restaurantRepository;
            private readonly IMediator _mediator;

            public UpdateRestaurantCommandHandler(IRestaurantRepository restaurantRepository, IMediator mediator)
            {
                _restaurantRepository = restaurantRepository;
                _mediator = mediator;
            }

            [ValidationAspect(typeof(UpdateRestaurantValidator), Priority = 1)]
            [CacheRemoveAspect("Get")]
            [LogAspect(typeof(FileLogger))]
            [SecuredOperation(Priority = 1)]
            public async Task<IResult> Handle(UpdateRestaurantCommand request, CancellationToken cancellationToken)
            {
                var isThereRestaurantRecord = await _restaurantRepository.GetAsync(u => u.Id == request.Id);


                isThereRestaurantRecord.UserId = request.UserId;
                isThereRestaurantRecord.Name = request.Name;
                isThereRestaurantRecord.Phone = request.Phone;
                isThereRestaurantRecord.Email = request.Email;
                isThereRestaurantRecord.Detail = request.Detail;
                isThereRestaurantRecord.CreateDate = request.CreateDate;
                isThereRestaurantRecord.Active = request.Active;


                _restaurantRepository.Update(isThereRestaurantRecord);
                await _restaurantRepository.SaveChangesAsync();
                return new SuccessResult(Messages.Updated);
            }
        }
    }
}

