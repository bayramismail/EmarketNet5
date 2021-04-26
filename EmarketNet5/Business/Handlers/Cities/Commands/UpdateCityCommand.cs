
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
using Business.Handlers.Cities.ValidationRules;


namespace Business.Handlers.Cities.Commands
{


    public class UpdateCityCommand : IRequest<IResult>
    {
        public int Id { get; set; }
        public int CountryId { get; set; }
        public string Name { get; set; }
        public System.DateTime CreateDate { get; set; }
        public bool Active { get; set; }

        public class UpdateCityCommandHandler : IRequestHandler<UpdateCityCommand, IResult>
        {
            private readonly ICityRepository _cityRepository;
            private readonly IMediator _mediator;

            public UpdateCityCommandHandler(ICityRepository cityRepository, IMediator mediator)
            {
                _cityRepository = cityRepository;
                _mediator = mediator;
            }

            [ValidationAspect(typeof(UpdateCityValidator), Priority = 1)]
            [CacheRemoveAspect("Get")]
            [LogAspect(typeof(FileLogger))]
            [SecuredOperation(Priority = 1)]
            public async Task<IResult> Handle(UpdateCityCommand request, CancellationToken cancellationToken)
            {
                var isThereCityRecord = await _cityRepository.GetAsync(u => u.Id == request.Id);


                isThereCityRecord.CountryId = request.CountryId;
                isThereCityRecord.Name = request.Name;
                isThereCityRecord.CreateDate = request.CreateDate;
                isThereCityRecord.Active = request.Active;


                _cityRepository.Update(isThereCityRecord);
                await _cityRepository.SaveChangesAsync();
                return new SuccessResult(Messages.Updated);
            }
        }
    }
}

