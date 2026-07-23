using MediatR;
using ECommerce.Application.Features.Cart.DTOs;

namespace ECommerce.Application.Features.Cart.Queries.GetCart
{
    public sealed record GetCartQuery()
        : IRequest<Result<CartDto>>;
}
