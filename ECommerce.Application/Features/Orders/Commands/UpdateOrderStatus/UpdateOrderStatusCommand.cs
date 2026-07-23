using MediatR;

namespace ECommerce.Application.Features.Orders.Commands.ChangeOrderStatus
{
    public sealed record UpdateOrderStatusCommand(
        int OrderId,
        enOrder.Status Status)
        : IRequest<Result>;
}
