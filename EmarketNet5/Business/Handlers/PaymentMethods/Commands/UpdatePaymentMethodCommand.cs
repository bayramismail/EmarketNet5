
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
using Business.Handlers.PaymentMethods.ValidationRules;


namespace Business.Handlers.PaymentMethods.Commands
{


    public class UpdatePaymentMethodCommand : IRequest<IResult>
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public System.DateTime CreateDate { get; set; }
        public bool Active { get; set; }

        public class UpdatePaymentMethodCommandHandler : IRequestHandler<UpdatePaymentMethodCommand, IResult>
        {
            private readonly IPaymentMethodRepository _paymentMethodRepository;
            private readonly IMediator _mediator;

            public UpdatePaymentMethodCommandHandler(IPaymentMethodRepository paymentMethodRepository, IMediator mediator)
            {
                _paymentMethodRepository = paymentMethodRepository;
                _mediator = mediator;
            }

            [ValidationAspect(typeof(UpdatePaymentMethodValidator), Priority = 1)]
            [CacheRemoveAspect("Get")]
            [LogAspect(typeof(FileLogger))]
            [SecuredOperation(Priority = 1)]
            public async Task<IResult> Handle(UpdatePaymentMethodCommand request, CancellationToken cancellationToken)
            {
                var isTherePaymentMethodRecord = await _paymentMethodRepository.GetAsync(u => u.Id == request.Id);


                isTherePaymentMethodRecord.Name = request.Name;
                isTherePaymentMethodRecord.CreateDate = request.CreateDate;
                isTherePaymentMethodRecord.Active = request.Active;


                _paymentMethodRepository.Update(isTherePaymentMethodRecord);
                await _paymentMethodRepository.SaveChangesAsync();
                return new SuccessResult(Messages.Updated);
            }
        }
    }
}

