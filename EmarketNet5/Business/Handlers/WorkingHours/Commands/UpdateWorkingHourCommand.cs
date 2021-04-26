
using System;
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
using Business.Handlers.WorkingHours.ValidationRules;


namespace Business.Handlers.WorkingHours.Commands
{


    public class UpdateWorkingHourCommand : IRequest<IResult>
    {
        public int Id { get; set; }
        public int RestaurantId { get; set; }
        public string Description { get; set; }
        public bool Active { get; set; }
        public class UpdateWorkingHourCommandHandler : IRequestHandler<UpdateWorkingHourCommand, IResult>
        {
            private readonly IWorkingHourRepository _workingHourRepository;
            private readonly IMediator _mediator;

            public UpdateWorkingHourCommandHandler(IWorkingHourRepository workingHourRepository, IMediator mediator)
            {
                _workingHourRepository = workingHourRepository;
                _mediator = mediator;
            }

            [ValidationAspect(typeof(UpdateWorkingHourValidator), Priority = 1)]
            [CacheRemoveAspect("Get")]
            [LogAspect(typeof(FileLogger))]
            [SecuredOperation(Priority = 1)]
            public async Task<IResult> Handle(UpdateWorkingHourCommand request, CancellationToken cancellationToken)
            {
                var isThereWorkingHourRecord = await _workingHourRepository.GetAsync(u => u.Id == request.Id);

                isThereWorkingHourRecord.RestaurantId = request.RestaurantId;
                isThereWorkingHourRecord.Description = request.Description;
                isThereWorkingHourRecord.CreateDate=DateTime.Now;
                isThereWorkingHourRecord.Active = request.Active;

                _workingHourRepository.Update(isThereWorkingHourRecord);
                await _workingHourRepository.SaveChangesAsync();
                return new SuccessResult(Messages.Updated);
            }
        }
    }
}

