
using Business.Handlers.RestaurantAddresses.Commands;
using FluentValidation;

namespace Business.Handlers.RestaurantAddresses.ValidationRules
{

    public class CreateRestaurantAddressValidator : AbstractValidator<CreateRestaurantAddressCommand>
    {
        public CreateRestaurantAddressValidator()
        {
            RuleFor(x => x.RestaurantId).NotEmpty();
            RuleFor(x => x.AddressTitle).NotEmpty();
            RuleFor(x => x.OpenAddress).NotEmpty();

        }
    }
    public class UpdateRestaurantAddressValidator : AbstractValidator<UpdateRestaurantAddressCommand>
    {
        public UpdateRestaurantAddressValidator()
        {
            RuleFor(x => x.RestaurantId).NotEmpty();
            RuleFor(x => x.AddressTitle).NotEmpty();
            RuleFor(x => x.OpenAddress).NotEmpty();

        }
    }
}