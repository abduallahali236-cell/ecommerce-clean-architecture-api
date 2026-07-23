namespace ECommerce.Application.Features.Payments.Commands.ConfirmPayment;

public sealed record ConfirmPaymentCommand(
    int PaymentId)
    : IRequest<Result>;