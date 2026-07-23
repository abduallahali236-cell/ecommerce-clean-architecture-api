using MediatR;

namespace ECommerce.Application.Features.Orders.Commands.DeleteOrder
{
    public sealed record CancelOrderCommand(int OrderId)
        : IRequest<Result>;
}
