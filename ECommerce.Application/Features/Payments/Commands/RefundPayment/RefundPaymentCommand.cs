namespace ECommerce.Application.Features.Payments.Commands.RefundPayment;

public sealed record RefundPaymentCommand(
    int PaymentId)
    : IRequest<Result>;