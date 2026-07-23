namespace ECommerce.Application.Features.Payments.Queries.GetPayment;
using ECommerce.Application.Features.Payments.DTOs;
public sealed record GetPaymentQuery(
    int PaymentId)
    : IRequest<Result<PaymentDto>>;