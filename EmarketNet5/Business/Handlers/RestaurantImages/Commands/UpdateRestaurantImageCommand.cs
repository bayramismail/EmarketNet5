
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
using Business.Handlers.RestaurantImages.ValidationRules;


namespace Business.Handlers.RestaurantImages.Commands
{


    public class UpdateRestaurantImageCommand : IRequest<IResult>
    {
        public int Id { get; set; }
        public int RestaurantId { get; set; }
        public string ImagePath { get; set; }
        public bool Active { get; set; }

        public class UpdateRestaurantImageCommandHandler : IRequestHandler<UpdateRestaurantImageCommand, IResult>
        {
            private readonly IRestaurantImageRepository _restaurantImageRepository;
            private readonly IMediator _mediator;

            public UpdateRestaurantImageCommandHandler(IRestaurantImageRepository restaurantImageRepository, IMediator mediator)
            {
                _restaurantImageRepository = restaurantImageRepository;
                _mediator = mediator;
            }

            [ValidationAspect(typeof(UpdateRestaurantImageValidator), Priority = 1)]
            [CacheRemoveAspect("Get")]
            [LogAspect(typeof(FileLogger))]
            [SecuredOperation(Priority = 1)]
            public async Task<IResult> Handle(UpdateRestaurantImageCommand request, CancellationToken cancellationToken)
            {
                var isThereRestaurantImageRecord = await _restaurantImageRepository.GetAsync(u => u.Id == request.Id);


                isThereRestaurantImageRecord.RestaurantId = request.RestaurantId;
                isThereRestaurantImageRecord.ImagePath = request.ImagePath;
                isThereRestaurantImageRecord.CreateDate=DateTime.Now;
                isThereRestaurantImageRecord.Active = request.Active;


                _restaurantImageRepository.Update(isThereRestaurantImageRecord);
                await _restaurantImageRepository.SaveChangesAsync();
                return new SuccessResult(Messages.Updated);
            }
        }
    }
}

