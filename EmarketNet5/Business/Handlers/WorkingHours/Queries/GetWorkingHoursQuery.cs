
using Business.BusinessAspects;
using Core.Aspects.Autofac.Performance;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.Concrete;
using MediatR;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Core.Aspects.Autofac.Logging;
using Core.CrossCuttingConcerns.Logging.Serilog.Loggers;
using Core.Aspects.Autofac.Caching;

namespace Business.Handlers.WorkingHours.Queries
{

    public class GetWorkingHoursQuery : IRequest<IDataResult<IEnumerable<WorkingHour>>>
    {
        public class GetWorkingHoursQueryHandler : IRequestHandler<GetWorkingHoursQuery, IDataResult<IEnumerable<WorkingHour>>>
        {
            private readonly IWorkingHourRepository _workingHourRepository;
            private readonly IMediator _mediator;

            public GetWorkingHoursQueryHandler(IWorkingHourRepository workingHourRepository, IMediator mediator)
            {
                _workingHourRepository = workingHourRepository;
                _mediator = mediator;
            }

            [PerformanceAspect(5)]
            [CacheAspect(10)]
            [LogAspect(typeof(FileLogger))]
            [SecuredOperation(Priority = 1)]
            public async Task<IDataResult<IEnumerable<WorkingHour>>> Handle(GetWorkingHoursQuery request, CancellationToken cancellationToken)
            {
                return new SuccessDataResult<IEnumerable<WorkingHour>>(await _workingHourRepository.GetListAsync());
            }
        }
    }
}