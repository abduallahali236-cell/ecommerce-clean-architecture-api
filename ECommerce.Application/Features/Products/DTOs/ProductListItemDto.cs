using System;

namespace ECommerce.Application.Features.Products.DTOs
{
    public sealed record ProductListItemDto(

        int Id,

        string Name,

        decimal Price,

        string Category,

        string? ImageUrl);
}
