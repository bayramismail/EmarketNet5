
using Business.Handlers.RestaurantImages.Commands;
using FluentValidation;

namespace Business.Handlers.RestaurantImages.ValidationRules
{

    public class CreateRestaurantImageValidator : AbstractValidator<CreateRestaurantImageCommand>
    {
        public CreateRestaurantImageValidator()
        {
            RuleFor(x => x.RestaurantId).NotEmpty();
            RuleFor(x => x.ImagePath).NotEmpty();
            RuleFor(x => x.DateOfUpload).NotEmpty();

        }
    }
    public class UpdateRestaurantImageValidator : AbstractValidator<UpdateRestaurantImageCommand>
    {
        public UpdateRestaurantImageValidator()
        {
            RuleFor(x => x.RestaurantId).NotEmpty();
            RuleFor(x => x.ImagePath).NotEmpty();
            RuleFor(x => x.DateOfUpload).NotEmpty();

        }
    }
}