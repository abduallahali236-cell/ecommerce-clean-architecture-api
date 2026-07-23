namespace ECommerce.Application.Features.Payments.DTOs;

public sealed record PaymentDto(
    int Id,
    int OrderId,
    enPayment.Method PaymentMethod,
    enPayment.Status Status,
    decimal Amount,
    DateTime CreatedAt);