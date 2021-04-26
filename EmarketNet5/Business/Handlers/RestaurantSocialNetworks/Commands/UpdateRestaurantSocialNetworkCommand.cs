
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
using Business.Handlers.RestaurantSocialNetworks.ValidationRules;


namespace Business.Handlers.RestaurantSocialNetworks.Commands
{


    public class UpdateRestaurantSocialNetworkCommand : IRequest<IResult>
    {
        public int Id { get; set; }
        public int RestaurantId { get; set; }
        public int SocialNetworkId { get; set; }
        public string Path { get; set; }
        public System.DateTime CreateDate { get; set; }
        public bool Active { get; set; }

        public class UpdateRestaurantSocialNetworkCommandHandler : IRequestHandler<UpdateRestaurantSocialNetworkCommand, IResult>
        {
            private readonly IRestaurantSocialNetworkRepository _restaurantSocialNetworkRepository;
            private readonly IMediator _mediator;

            public UpdateRestaurantSocialNetworkCommandHandler(IRestaurantSocialNetworkRepository restaurantSocialNetworkRepository, IMediator mediator)
            {
                _restaurantSocialNetworkRepository = restaurantSocialNetworkRepository;
                _mediator = mediator;
            }

            [ValidationAspect(typeof(UpdateRestaurantSocialNetworkValidator), Priority = 1)]
            [CacheRemoveAspect("Get")]
            [LogAspect(typeof(FileLogger))]
            [SecuredOperation(Priority = 1)]
            public async Task<IResult> Handle(UpdateRestaurantSocialNetworkCommand request, CancellationToken cancellationToken)
            {
                var isThereRestaurantSocialNetworkRecord = await _restaurantSocialNetworkRepository.GetAsync(u => u.Id == request.Id);


                isThereRestaurantSocialNetworkRecord.RestaurantId = request.RestaurantId;
                isThereRestaurantSocialNetworkRecord.SocialNetworkId = request.SocialNetworkId;
                isThereRestaurantSocialNetworkRecord.Path = request.Path;
                isThereRestaurantSocialNetworkRecord.CreateDate = request.CreateDate;
                isThereRestaurantSocialNetworkRecord.Active = request.Active;


                _restaurantSocialNetworkRepository.Update(isThereRestaurantSocialNetworkRecord);
                await _restaurantSocialNetworkRepository.SaveChangesAsync();
                return new SuccessResult(Messages.Updated);
            }
        }
    }
}

