using MediatR;

namespace ECommerce.Application.Features.Cart.Commands.ClearCart
{
    public sealed record ClearCartCommand()
        : IRequest<Result>;
}
