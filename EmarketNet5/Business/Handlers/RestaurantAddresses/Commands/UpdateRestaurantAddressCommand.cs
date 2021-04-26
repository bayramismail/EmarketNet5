
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
using Business.Handlers.RestaurantAddresses.ValidationRules;


namespace Business.Handlers.RestaurantAddresses.Commands
{


    public class UpdateRestaurantAddressCommand : IRequest<IResult>
    {
        public int Id { get; set; }
        public int RestaurantId { get; set; }
        public string AddressTitle { get; set; }
        public string OpenAddress { get; set; }
        public bool Active { get; set; }
        public class UpdateRestaurantAddressCommandHandler : IRequestHandler<UpdateRestaurantAddressCommand, IResult>
        {
            private readonly IRestaurantAddressRepository _restaurantAddressRepository;
            private readonly IMediator _mediator;

            public UpdateRestaurantAddressCommandHandler(IRestaurantAddressRepository restaurantAddressRepository, IMediator mediator)
            {
                _restaurantAddressRepository = restaurantAddressRepository;
                _mediator = mediator;
            }

            [ValidationAspect(typeof(UpdateRestaurantAddressValidator), Priority = 1)]
            [CacheRemoveAspect("Get")]
            [LogAspect(typeof(FileLogger))]
            [SecuredOperation(Priority = 1)]
            public async Task<IResult> Handle(UpdateRestaurantAddressCommand request, CancellationToken cancellationToken)
            {
                var isThereRestaurantAddressRecord = await _restaurantAddressRepository.GetAsync(u => u.Id == request.Id);


                isThereRestaurantAddressRecord.RestaurantId = request.RestaurantId;
                isThereRestaurantAddressRecord.AddressTitle = request.AddressTitle;
                isThereRestaurantAddressRecord.OpenAddress = request.OpenAddress;
                isThereRestaurantAddressRecord.CreateDate =DateTime.Now;
                isThereRestaurantAddressRecord.Active = request.Active;

                _restaurantAddressRepository.Update(isThereRestaurantAddressRecord);
                await _restaurantAddressRepository.SaveChangesAsync();
                return new SuccessResult(Messages.Updated);
            }
        }
    }
}

