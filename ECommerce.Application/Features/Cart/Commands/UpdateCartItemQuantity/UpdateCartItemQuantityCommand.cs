using MediatR;

namespace ECommerce.Application.Features.Cart.Commands.UpdateCartItem
{
    public sealed record UpdateCartItemQuantityCommand(
        int ProductId,
        int Quantity)
        : IRequest<Result>;
}
