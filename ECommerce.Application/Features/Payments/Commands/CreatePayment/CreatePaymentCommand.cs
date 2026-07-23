namespace ECommerce.Application.Features.Payments.Commands.CreatePayment;

public sealed record CreatePaymentCommand(
    int OrderId,
    decimal Amount,
    enPayment.Method PaymentMethod)
    : IRequest<Result<int>>;