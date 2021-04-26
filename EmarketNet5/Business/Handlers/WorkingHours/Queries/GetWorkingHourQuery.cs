
using Business.BusinessAspects;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.Concrete;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using Core.Aspects.Autofac.Logging;
using Core.CrossCuttingConcerns.Logging.Serilog.Loggers;


namespace Business.Handlers.WorkingHours.Queries
{
    public class GetWorkingHourQuery : IRequest<IDataResult<WorkingHour>>
    {
        public int Id { get; set; }

        public class GetWorkingHourQueryHandler : IRequestHandler<GetWorkingHourQuery, IDataResult<WorkingHour>>
        {
            private readonly IWorkingHourRepository _workingHourRepository;
            private readonly IMediator _mediator;

            public GetWorkingHourQueryHandler(IWorkingHourRepository workingHourRepository, IMediator mediator)
            {
                _workingHourRepository = workingHourRepository;
                _mediator = mediator;
            }
            [LogAspect(typeof(FileLogger))]
            [SecuredOperation(Priority = 1)]
            public async Task<IDataResult<WorkingHour>> Handle(GetWorkingHourQuery request, CancellationToken cancellationToken)
            {
                var workingHour = await _workingHourRepository.GetAsync(p => p.Id == request.Id);
                return new SuccessDataResult<WorkingHour>(workingHour);
            }
        }
    }
}
