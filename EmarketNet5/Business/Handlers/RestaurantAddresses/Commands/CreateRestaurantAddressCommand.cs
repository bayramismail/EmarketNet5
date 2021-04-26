
using System;
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
using Business.Handlers.RestaurantAddresses.ValidationRules;

namespace Business.Handlers.RestaurantAddresses.Commands
{
    /// <summary>
    /// 
    /// </summary>
    public class CreateRestaurantAddressCommand : IRequest<IResult>
    {

        public int RestaurantId { get; set; }
        public string AddressTitle { get; set; }
        public string OpenAddress { get; set; }
        public bool Active { get; set; }

        public class CreateRestaurantAddressCommandHandler : IRequestHandler<CreateRestaurantAddressCommand, IResult>
        {
            private readonly IRestaurantAddressRepository _restaurantAddressRepository;
            private readonly IMediator _mediator;
            public CreateRestaurantAddressCommandHandler(IRestaurantAddressRepository restaurantAddressRepository, IMediator mediator)
            {
                _restaurantAddressRepository = restaurantAddressRepository;
                _mediator = mediator;
            }

            [ValidationAspect(typeof(CreateRestaurantAddressValidator), Priority = 1)]
            [CacheRemoveAspect("Get")]
            [LogAspect(typeof(FileLogger))]
            [SecuredOperation(Priority = 1)]
            public async Task<IResult> Handle(CreateRestaurantAddressCommand request, CancellationToken cancellationToken)
            {
                var isThereRestaurantAddressRecord = _restaurantAddressRepository.Query().Any(u => u.RestaurantId == request.RestaurantId);

                if (isThereRestaurantAddressRecord == true)
                    return new ErrorResult(Messages.NameAlreadyExist);

                var addedRestaurantAddress = new RestaurantAddress
                {
                    RestaurantId = request.RestaurantId,
                    AddressTitle = request.AddressTitle,
                    OpenAddress = request.OpenAddress,
                    CreateDate = DateTime.Now,
                    Active = request.Active

                };

                _restaurantAddressRepository.Add(addedRestaurantAddress);
                await _restaurantAddressRepository.SaveChangesAsync();
                return new SuccessResult(Messages.Added);
            }
        }
    }
}