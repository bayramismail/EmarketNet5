
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
using Business.Handlers.SocialNetworks.ValidationRules;


namespace Business.Handlers.SocialNetworks.Commands
{


    public class UpdateSocialNetworkCommand : IRequest<IResult>
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public System.DateTime CreateDate { get; set; }
        public bool Active { get; set; }

        public class UpdateSocialNetworkCommandHandler : IRequestHandler<UpdateSocialNetworkCommand, IResult>
        {
            private readonly ISocialNetworkRepository _socialNetworkRepository;
            private readonly IMediator _mediator;

            public UpdateSocialNetworkCommandHandler(ISocialNetworkRepository socialNetworkRepository, IMediator mediator)
            {
                _socialNetworkRepository = socialNetworkRepository;
                _mediator = mediator;
            }

            [ValidationAspect(typeof(UpdateSocialNetworkValidator), Priority = 1)]
            [CacheRemoveAspect("Get")]
            [LogAspect(typeof(FileLogger))]
            [SecuredOperation(Priority = 1)]
            public async Task<IResult> Handle(UpdateSocialNetworkCommand request, CancellationToken cancellationToken)
            {
                var isThereSocialNetworkRecord = await _socialNetworkRepository.GetAsync(u => u.Id == request.Id);


                isThereSocialNetworkRecord.Name = request.Name;
                isThereSocialNetworkRecord.CreateDate = request.CreateDate;
                isThereSocialNetworkRecord.Active = request.Active;


                _socialNetworkRepository.Update(isThereSocialNetworkRecord);
                await _socialNetworkRepository.SaveChangesAsync();
                return new SuccessResult(Messages.Updated);
            }
        }
    }
}

