
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


namespace Business.Handlers.PaymentMethods.Commands
{
    /// <summary>
    /// 
    /// </summary>
    public class DeletePaymentMethodCommand : IRequest<IResult>
    {
        public int Id { get; set; }

        public class DeletePaymentMethodCommandHandler : IRequestHandler<DeletePaymentMethodCommand, IResult>
        {
            private readonly IPaymentMethodRepository _paymentMethodRepository;
            private readonly IMediator _mediator;

            public DeletePaymentMethodCommandHandler(IPaymentMethodRepository paymentMethodRepository, IMediator mediator)
            {
                _paymentMethodRepository = paymentMethodRepository;
                _mediator = mediator;
            }

            [CacheRemoveAspect("Get")]
            [LogAspect(typeof(FileLogger))]
            [SecuredOperation(Priority = 1)]
            public async Task<IResult> Handle(DeletePaymentMethodCommand request, CancellationToken cancellationToken)
            {
                var paymentMethodToDelete = _paymentMethodRepository.Get(p => p.Id == request.Id);

                _paymentMethodRepository.Delete(paymentMethodToDelete);
                await _paymentMethodRepository.SaveChangesAsync();
                return new SuccessResult(Messages.Deleted);
            }
        }
    }
}

