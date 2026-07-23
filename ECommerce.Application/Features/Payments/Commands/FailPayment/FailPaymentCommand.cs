namespace ECommerce.Application.Features.Payments.Commands.FailPayment; 

public sealed record FailPaymentCommand(
    int PaymentId)
    : IRequest<Result>;