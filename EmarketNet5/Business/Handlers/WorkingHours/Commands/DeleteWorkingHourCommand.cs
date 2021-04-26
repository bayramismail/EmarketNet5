
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


namespace Business.Handlers.WorkingHours.Commands
{
    /// <summary>
    /// 
    /// </summary>
    public class DeleteWorkingHourCommand : IRequest<IResult>
    {
        public int Id { get; set; }

        public class DeleteWorkingHourCommandHandler : IRequestHandler<DeleteWorkingHourCommand, IResult>
        {
            private readonly IWorkingHourRepository _workingHourRepository;
            private readonly IMediator _mediator;

            public DeleteWorkingHourCommandHandler(IWorkingHourRepository workingHourRepository, IMediator mediator)
            {
                _workingHourRepository = workingHourRepository;
                _mediator = mediator;
            }

            [CacheRemoveAspect("Get")]
            [LogAspect(typeof(FileLogger))]
            [SecuredOperation(Priority = 1)]
            public async Task<IResult> Handle(DeleteWorkingHourCommand request, CancellationToken cancellationToken)
            {
                var workingHourToDelete = _workingHourRepository.Get(p => p.Id == request.Id);

                _workingHourRepository.Delete(workingHourToDelete);
                await _workingHourRepository.SaveChangesAsync();
                return new SuccessResult(Messages.Deleted);
            }
        }
    }
}

