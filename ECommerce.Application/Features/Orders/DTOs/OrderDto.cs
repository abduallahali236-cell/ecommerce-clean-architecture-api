using System;
using System.Collections.Generic;

namespace ECommerce.Application.Features.Orders.DTOs
{
    public sealed record OrderDto(
        int Id,
        decimal TotalAmount,
        enOrder.Status Status,
        DateTime CreatedAt,
        IReadOnlyCollection<OrderItemDto> Items);
}
