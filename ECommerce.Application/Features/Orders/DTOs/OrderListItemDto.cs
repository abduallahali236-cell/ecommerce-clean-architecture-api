using System;

namespace ECommerce.Application.Features.Orders.DTOs
{
    public sealed record OrderListItemDto(
        int Id,
        decimal TotalAmount,
        enOrder.Status Status,
        DateTime CreatedAt);
}
