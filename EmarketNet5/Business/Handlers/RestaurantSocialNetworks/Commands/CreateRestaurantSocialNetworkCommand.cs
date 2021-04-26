
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
using Business.Handlers.RestaurantSocialNetworks.ValidationRules;

namespace Business.Handlers.RestaurantSocialNetworks.Commands
{
    /// <summary>
    /// 
    /// </summary>
    public class CreateRestaurantSocialNetworkCommand : IRequest<IResult>
    {

        public int RestaurantId { get; set; }
        public int SocialNetworkId { get; set; }
        public string Path { get; set; }
        public System.DateTime CreateDate { get; set; }
        public bool Active { get; set; }


        public class CreateRestaurantSocialNetworkCommandHandler : IRequestHandler<CreateRestaurantSocialNetworkCommand, IResult>
        {
            private readonly IRestaurantSocialNetworkRepository _restaurantSocialNetworkRepository;
            private readonly IMediator _mediator;
            public CreateRestaurantSocialNetworkCommandHandler(IRestaurantSocialNetworkRepository restaurantSocialNetworkRepository, IMediator mediator)
            {
                _restaurantSocialNetworkRepository = restaurantSocialNetworkRepository;
                _mediator = mediator;
            }

            [ValidationAspect(typeof(CreateRestaurantSocialNetworkValidator), Priority = 1)]
            [CacheRemoveAspect("Get")]
            [LogAspect(typeof(FileLogger))]
            [SecuredOperation(Priority = 1)]
            public async Task<IResult> Handle(CreateRestaurantSocialNetworkCommand request, CancellationToken cancellationToken)
            {
                var isThereRestaurantSocialNetworkRecord = _restaurantSocialNetworkRepository.Query().Any(u => u.RestaurantId == request.RestaurantId);

                if (isThereRestaurantSocialNetworkRecord == true)
                    return new ErrorResult(Messages.NameAlreadyExist);

                var addedRestaurantSocialNetwork = new RestaurantSocialNetwork
                {
                    RestaurantId = request.RestaurantId,
                    SocialNetworkId = request.SocialNetworkId,
                    Path = request.Path,
                    CreateDate = request.CreateDate,
                    Active = request.Active,

                };

                _restaurantSocialNetworkRepository.Add(addedRestaurantSocialNetwork);
                await _restaurantSocialNetworkRepository.SaveChangesAsync();
                return new SuccessResult(Messages.Added);
            }
        }
    }
}