
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
using Business.Handlers.WorkingHours.ValidationRules;

namespace Business.Handlers.WorkingHours.Commands
{
    /// <summary>
    /// 
    /// </summary>
    public class CreateWorkingHourCommand : IRequest<IResult>
    {

        public string Description { get; set; }


        public class CreateWorkingHourCommandHandler : IRequestHandler<CreateWorkingHourCommand, IResult>
        {
            private readonly IWorkingHourRepository _workingHourRepository;
            private readonly IMediator _mediator;
            public CreateWorkingHourCommandHandler(IWorkingHourRepository workingHourRepository, IMediator mediator)
            {
                _workingHourRepository = workingHourRepository;
                _mediator = mediator;
            }

            [ValidationAspect(typeof(CreateWorkingHourValidator), Priority = 1)]
            [CacheRemoveAspect("Get")]
            [LogAspect(typeof(FileLogger))]
            [SecuredOperation(Priority = 1)]
            public async Task<IResult> Handle(CreateWorkingHourCommand request, CancellationToken cancellationToken)
            {
                var isThereWorkingHourRecord = _workingHourRepository.Query().Any(u => u.Description == request.Description);

                if (isThereWorkingHourRecord == true)
                    return new ErrorResult(Messages.NameAlreadyExist);

                var addedWorkingHour = new WorkingHour
                {
                    Description = request.Description,

                };

                _workingHourRepository.Add(addedWorkingHour);
                await _workingHourRepository.SaveChangesAsync();
                return new SuccessResult(Messages.Added);
            }
        }
    }
}