using MediatR;

namespace ECommerce.Application.Features.Cart.Commands.RemoveFromCart
{
    public sealed record RemoveFromCartCommand(int ProductId)
        : IRequest<Result>;
}
