
using Business.BusinessAspects;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.Concrete;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using Core.Aspects.Autofac.Logging;
using Core.CrossCuttingConcerns.Logging.Serilog.Loggers;


namespace Business.Handlers.SocialNetworks.Queries
{
    public class GetSocialNetworkQuery : IRequest<IDataResult<SocialNetwork>>
    {
        public int Id { get; set; }

        public class GetSocialNetworkQueryHandler : IRequestHandler<GetSocialNetworkQuery, IDataResult<SocialNetwork>>
        {
            private readonly ISocialNetworkRepository _socialNetworkRepository;
            private readonly IMediator _mediator;

            public GetSocialNetworkQueryHandler(ISocialNetworkRepository socialNetworkRepository, IMediator mediator)
            {
                _socialNetworkRepository = socialNetworkRepository;
                _mediator = mediator;
            }
            [LogAspect(typeof(FileLogger))]
            [SecuredOperation(Priority = 1)]
            public async Task<IDataResult<SocialNetwork>> Handle(GetSocialNetworkQuery request, CancellationToken cancellationToken)
            {
                var socialNetwork = await _socialNetworkRepository.GetAsync(p => p.Id == request.Id);
                return new SuccessDataResult<SocialNetwork>(socialNetwork);
            }
        }
    }
}
