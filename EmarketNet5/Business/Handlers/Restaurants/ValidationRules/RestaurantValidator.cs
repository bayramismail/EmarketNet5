
using Business.Handlers.Restaurants.Commands;
using FluentValidation;

namespace Business.Handlers.Restaurants.ValidationRules
{

    public class CreateRestaurantValidator : AbstractValidator<CreateRestaurantCommand>
    {
        public CreateRestaurantValidator()
        {
            RuleFor(x => x.UserId).NotEmpty();
            RuleFor(x => x.Name).NotEmpty();
            RuleFor(x => x.Phone).NotEmpty();
            RuleFor(x => x.Email).NotEmpty();
            RuleFor(x => x.Detail).NotEmpty();
            RuleFor(x => x.CreateDate).NotEmpty();
            RuleFor(x => x.Active).NotEmpty();

        }
    }
    public class UpdateRestaurantValidator : AbstractValidator<UpdateRestaurantCommand>
    {
        public UpdateRestaurantValidator()
        {
            RuleFor(x => x.UserId).NotEmpty();
            RuleFor(x => x.Name).NotEmpty();
            RuleFor(x => x.Phone).NotEmpty();
            RuleFor(x => x.Email).NotEmpty();
            RuleFor(x => x.Detail).NotEmpty();
            RuleFor(x => x.CreateDate).NotEmpty();
            RuleFor(x => x.Active).NotEmpty();

        }
    }
}