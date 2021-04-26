
using Business.Handlers.SocialNetworks.Commands;
using FluentValidation;

namespace Business.Handlers.SocialNetworks.ValidationRules
{

    public class CreateSocialNetworkValidator : AbstractValidator<CreateSocialNetworkCommand>
    {
        public CreateSocialNetworkValidator()
        {
            RuleFor(x => x.Name).NotEmpty();
            RuleFor(x => x.CreateDate).NotEmpty();
            RuleFor(x => x.Active).NotEmpty();

        }
    }
    public class UpdateSocialNetworkValidator : AbstractValidator<UpdateSocialNetworkCommand>
    {
        public UpdateSocialNetworkValidator()
        {
            RuleFor(x => x.Name).NotEmpty();
            RuleFor(x => x.CreateDate).NotEmpty();
            RuleFor(x => x.Active).NotEmpty();

        }
    }
}