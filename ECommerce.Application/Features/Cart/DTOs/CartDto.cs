using System.Collections.Generic;

namespace ECommerce.Application.Features.Cart.DTOs
{
    public sealed record CartDto(
        int Id,
        IReadOnlyCollection<CartItemDto> Items,
        decimal TotalPrice,
        int TotalItems);
}
