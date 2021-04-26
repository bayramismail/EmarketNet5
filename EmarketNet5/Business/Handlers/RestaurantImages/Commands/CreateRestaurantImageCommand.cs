
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
using Business.Handlers.RestaurantImages.ValidationRules;

namespace Business.Handlers.RestaurantImages.Commands
{
    /// <summary>
    /// 
    /// </summary>
    public class CreateRestaurantImageCommand : IRequest<IResult>
    {

        public int RestaurantId { get; set; }
        public string ImagePath { get; set; }
        public bool Active { get; set; }


        public class CreateRestaurantImageCommandHandler : IRequestHandler<CreateRestaurantImageCommand, IResult>
        {
            private readonly IRestaurantImageRepository _restaurantImageRepository;
            private readonly IMediator _mediator;
            public CreateRestaurantImageCommandHandler(IRestaurantImageRepository restaurantImageRepository, IMediator mediator)
            {
                _restaurantImageRepository = restaurantImageRepository;
                _mediator = mediator;
            }

            [ValidationAspect(typeof(CreateRestaurantImageValidator), Priority = 1)]
            [CacheRemoveAspect("Get")]
            [LogAspect(typeof(FileLogger))]
            [SecuredOperation(Priority = 1)]
            public async Task<IResult> Handle(CreateRestaurantImageCommand request, CancellationToken cancellationToken)
            {
                var isThereRestaurantImageRecord = _restaurantImageRepository.Query().Any(u => u.RestaurantId == request.RestaurantId);

                if (isThereRestaurantImageRecord == true)
                    return new ErrorResult(Messages.NameAlreadyExist);

                var addedRestaurantImage = new RestaurantImage
                {
                    RestaurantId = request.RestaurantId,
                    ImagePath = request.ImagePath,
                    CreateDate = DateTime.Now,
                    Active = request.Active

                };

                _restaurantImageRepository.Add(addedRestaurantImage);
                await _restaurantImageRepository.SaveChangesAsync();
                return new SuccessResult(Messages.Added);
            }
        }
    }
}