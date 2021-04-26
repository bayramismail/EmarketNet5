
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
using Business.Handlers.PaymentMethods.ValidationRules;

namespace Business.Handlers.PaymentMethods.Commands
{
    /// <summary>
    /// 
    /// </summary>
    public class CreatePaymentMethodCommand : IRequest<IResult>
    {

        public string Name { get; set; }
        public System.DateTime CreateDate { get; set; }
        public bool Active { get; set; }


        public class CreatePaymentMethodCommandHandler : IRequestHandler<CreatePaymentMethodCommand, IResult>
        {
            private readonly IPaymentMethodRepository _paymentMethodRepository;
            private readonly IMediator _mediator;
            public CreatePaymentMethodCommandHandler(IPaymentMethodRepository paymentMethodRepository, IMediator mediator)
            {
                _paymentMethodRepository = paymentMethodRepository;
                _mediator = mediator;
            }

            [ValidationAspect(typeof(CreatePaymentMethodValidator), Priority = 1)]
            [CacheRemoveAspect("Get")]
            [LogAspect(typeof(FileLogger))]
            [SecuredOperation(Priority = 1)]
            public async Task<IResult> Handle(CreatePaymentMethodCommand request, CancellationToken cancellationToken)
            {
                var isTherePaymentMethodRecord = _paymentMethodRepository.Query().Any(u => u.Name == request.Name);

                if (isTherePaymentMethodRecord == true)
                    return new ErrorResult(Messages.NameAlreadyExist);

                var addedPaymentMethod = new PaymentMethod
                {
                    Name = request.Name,
                    CreateDate = request.CreateDate,
                    Active = request.Active,

                };

                _paymentMethodRepository.Add(addedPaymentMethod);
                await _paymentMethodRepository.SaveChangesAsync();
                return new SuccessResult(Messages.Added);
            }
        }
    }
}