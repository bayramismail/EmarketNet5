
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

namespace Business.Handlers.PaymentMethods.Queries
{

    public class GetPaymentMethodsQuery : IRequest<IDataResult<IEnumerable<PaymentMethod>>>
    {
        public class GetPaymentMethodsQueryHandler : IRequestHandler<GetPaymentMethodsQuery, IDataResult<IEnumerable<PaymentMethod>>>
        {
            private readonly IPaymentMethodRepository _paymentMethodRepository;
            private readonly IMediator _mediator;

            public GetPaymentMethodsQueryHandler(IPaymentMethodRepository paymentMethodRepository, IMediator mediator)
            {
                _paymentMethodRepository = paymentMethodRepository;
                _mediator = mediator;
            }

            [PerformanceAspect(5)]
            [CacheAspect(10)]
            [LogAspect(typeof(FileLogger))]
            [SecuredOperation(Priority = 1)]
            public async Task<IDataResult<IEnumerable<PaymentMethod>>> Handle(GetPaymentMethodsQuery request, CancellationToken cancellationToken)
            {
                return new SuccessDataResult<IEnumerable<PaymentMethod>>(await _paymentMethodRepository.GetListAsync());
            }
        }
    }
}