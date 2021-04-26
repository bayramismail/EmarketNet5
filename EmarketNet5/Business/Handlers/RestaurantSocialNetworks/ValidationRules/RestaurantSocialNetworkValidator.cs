
using Business.Handlers.RestaurantSocialNetworks.Commands;
using FluentValidation;

namespace Business.Handlers.RestaurantSocialNetworks.ValidationRules
{

    public class CreateRestaurantSocialNetworkValidator : AbstractValidator<CreateRestaurantSocialNetworkCommand>
    {
        public CreateRestaurantSocialNetworkValidator()
        {
            RuleFor(x => x.RestaurantId).NotEmpty();
            RuleFor(x => x.SocialNetworkId).NotEmpty();
            RuleFor(x => x.Path).NotEmpty();
            RuleFor(x => x.CreateDate).NotEmpty();
            RuleFor(x => x.Active).NotEmpty();

        }
    }
    public class UpdateRestaurantSocialNetworkValidator : AbstractValidator<UpdateRestaurantSocialNetworkCommand>
    {
        public UpdateRestaurantSocialNetworkValidator()
        {
            RuleFor(x => x.RestaurantId).NotEmpty();
            RuleFor(x => x.SocialNetworkId).NotEmpty();
            RuleFor(x => x.Path).NotEmpty();
            RuleFor(x => x.CreateDate).NotEmpty();
            RuleFor(x => x.Active).NotEmpty();

        }
    }
}