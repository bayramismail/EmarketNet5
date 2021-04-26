
using Business.Handlers.WorkingHours.Commands;
using FluentValidation;

namespace Business.Handlers.WorkingHours.ValidationRules
{

    public class CreateWorkingHourValidator : AbstractValidator<CreateWorkingHourCommand>
    {
        public CreateWorkingHourValidator()
        {
            RuleFor(x => x.Description).NotEmpty();

        }
    }
    public class UpdateWorkingHourValidator : AbstractValidator<UpdateWorkingHourCommand>
    {
        public UpdateWorkingHourValidator()
        {
            RuleFor(x => x.Description).NotEmpty();

        }
    }
}