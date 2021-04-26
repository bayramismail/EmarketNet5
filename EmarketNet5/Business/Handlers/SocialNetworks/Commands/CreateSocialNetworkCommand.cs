
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
using Business.Handlers.SocialNetworks.ValidationRules;

namespace Business.Handlers.SocialNetworks.Commands
{
    /// <summary>
    /// 
    /// </summary>
    public class CreateSocialNetworkCommand : IRequest<IResult>
    {

        public string Name { get; set; }
        public System.DateTime CreateDate { get; set; }
        public bool Active { get; set; }


        public class CreateSocialNetworkCommandHandler : IRequestHandler<CreateSocialNetworkCommand, IResult>
        {
            private readonly ISocialNetworkRepository _socialNetworkRepository;
            private readonly IMediator _mediator;
            public CreateSocialNetworkCommandHandler(ISocialNetworkRepository socialNetworkRepository, IMediator mediator)
            {
                _socialNetworkRepository = socialNetworkRepository;
                _mediator = mediator;
            }

            [ValidationAspect(typeof(CreateSocialNetworkValidator), Priority = 1)]
            [CacheRemoveAspect("Get")]
            [LogAspect(typeof(FileLogger))]
            [SecuredOperation(Priority = 1)]
            public async Task<IResult> Handle(CreateSocialNetworkCommand request, CancellationToken cancellationToken)
            {
                var isThereSocialNetworkRecord = _socialNetworkRepository.Query().Any(u => u.Name == request.Name);

                if (isThereSocialNetworkRecord == true)
                    return new ErrorResult(Messages.NameAlreadyExist);

                var addedSocialNetwork = new SocialNetwork
                {
                    Name = request.Name,
                    CreateDate = request.CreateDate,
                    Active = request.Active,

                };

                _socialNetworkRepository.Add(addedSocialNetwork);
                await _socialNetworkRepository.SaveChangesAsync();
                return new SuccessResult(Messages.Added);
            }
        }
    }
}