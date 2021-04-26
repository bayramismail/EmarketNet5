
using Business.Constants;
using Core.Aspects.Autofac.Caching;
using Business.BusinessAspects;
using Core.Aspects.Autofac.Logging;
using Core.CrossCuttingConcerns.Logging.Serilog.Loggers;
using Core.Utilities.Results;
using DataAccess.Abstract;
using MediatR;
using System.Threading;
using System.Threading.Tasks;


namespace Business.Handlers.SocialNetworks.Commands
{
    /// <summary>
    /// 
    /// </summary>
    public class DeleteSocialNetworkCommand : IRequest<IResult>
    {
        public int Id { get; set; }

        public class DeleteSocialNetworkCommandHandler : IRequestHandler<DeleteSocialNetworkCommand, IResult>
        {
            private readonly ISocialNetworkRepository _socialNetworkRepository;
            private readonly IMediator _mediator;

            public DeleteSocialNetworkCommandHandler(ISocialNetworkRepository socialNetworkRepository, IMediator mediator)
            {
                _socialNetworkRepository = socialNetworkRepository;
                _mediator = mediator;
            }

            [CacheRemoveAspect("Get")]
            [LogAspect(typeof(FileLogger))]
            [SecuredOperation(Priority = 1)]
            public async Task<IResult> Handle(DeleteSocialNetworkCommand request, CancellationToken cancellationToken)
            {
                var socialNetworkToDelete = _socialNetworkRepository.Get(p => p.Id == request.Id);

                _socialNetworkRepository.Delete(socialNetworkToDelete);
                await _socialNetworkRepository.SaveChangesAsync();
                return new SuccessResult(Messages.Deleted);
            }
        }
    }
}

