using MediatR;
using ECommerce.Application.Features.Orders.DTOs;

namespace ECommerce.Application.Features.Orders.Queries.GetOrderById
{
    public sealed record GetOrderByIdQuery(int OrderId)
        : IRequest<Result<OrderDto>>;
}
