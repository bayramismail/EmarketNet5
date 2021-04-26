
using Business.Handlers.PaymentMethods.Commands;
using FluentValidation;

namespace Business.Handlers.PaymentMethods.ValidationRules
{

    public class CreatePaymentMethodValidator : AbstractValidator<CreatePaymentMethodCommand>
    {
        public CreatePaymentMethodValidator()
        {
            RuleFor(x => x.Name).NotEmpty();
            RuleFor(x => x.CreateDate).NotEmpty();
            RuleFor(x => x.Active).NotEmpty();

        }
    }
    public class UpdatePaymentMethodValidator : AbstractValidator<UpdatePaymentMethodCommand>
    {
        public UpdatePaymentMethodValidator()
        {
            RuleFor(x => x.Name).NotEmpty();
            RuleFor(x => x.CreateDate).NotEmpty();
            RuleFor(x => x.Active).NotEmpty();

        }
    }
}