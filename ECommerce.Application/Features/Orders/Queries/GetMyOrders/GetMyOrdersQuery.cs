using MediatR;
using System.Collections.Generic;
using ECommerce.Application.Features.Orders.DTOs;

namespace ECommerce.Application.Features.Orders.Queries.GetMyOrders
{
    public sealed record GetMyOrdersQuery(
        int PageNumber = 1,
        int PageSize = 10)
        : IRequest<Result<PaginatedResult<OrderListItemDto>>>;
}
