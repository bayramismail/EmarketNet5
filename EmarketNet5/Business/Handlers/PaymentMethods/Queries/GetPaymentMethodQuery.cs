
using Business.BusinessAspects;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.Concrete;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using Core.Aspects.Autofac.Logging;
using Core.CrossCuttingConcerns.Logging.Serilog.Loggers;


namespace Business.Handlers.PaymentMethods.Queries
{
    public class GetPaymentMethodQuery : IRequest<IDataResult<PaymentMethod>>
    {
        public int Id { get; set; }

        public class GetPaymentMethodQueryHandler : IRequestHandler<GetPaymentMethodQuery, IDataResult<PaymentMethod>>
        {
            private readonly IPaymentMethodRepository _paymentMethodRepository;
            private readonly IMediator _mediator;

            public GetPaymentMethodQueryHandler(IPaymentMethodRepository paymentMethodRepository, IMediator mediator)
            {
                _paymentMethodRepository = paymentMethodRepository;
                _mediator = mediator;
            }
            [LogAspect(typeof(FileLogger))]
            [SecuredOperation(Priority = 1)]
            public async Task<IDataResult<PaymentMethod>> Handle(GetPaymentMethodQuery request, CancellationToken cancellationToken)
            {
                var paymentMethod = await _paymentMethodRepository.GetAsync(p => p.Id == request.Id);
                return new SuccessDataResult<PaymentMethod>(paymentMethod);
            }
        }
    }
}
