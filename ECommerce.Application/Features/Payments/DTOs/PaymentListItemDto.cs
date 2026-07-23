namespace ECommerce.Application.Features.Payments.DTOs;

public sealed record PaymentListItemDto(
    int Id,
    int OrderId,
    decimal Amount,
    enPayment.Status Status);