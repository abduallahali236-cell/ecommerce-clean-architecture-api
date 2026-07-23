namespace ECommerce.Application.Features.Payments.Commands.CreatePayment;

public sealed class CreatePaymentValidator
    : AbstractValidator<CreatePaymentCommand>
{
    public CreatePaymentValidator()
    {
        RuleFor(x => x.OrderId)
            .GreaterThan(0);

        RuleFor(x => x.PaymentMethod)
            .IsInEnum();
    }
}