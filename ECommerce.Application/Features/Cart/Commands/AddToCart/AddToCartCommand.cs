using MediatR;

namespace ECommerce.Application.Features.Cart.Commands.AddToCart
{
    public sealed record AddToCartCommand(
        int ProductId,
        int Quantity)
        : IRequest<Result>;
}
