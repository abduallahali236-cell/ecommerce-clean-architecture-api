using ECommerce.Application.Features.Payments.DTOs;

public sealed record GetPaymentsQuery(
    int PageNumber = 1,
    int PageSize = 10)
    : IRequest<Result<PaginatedResult<PaymentListItemDto>>>;