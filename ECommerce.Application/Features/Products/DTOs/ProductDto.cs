using System;

namespace ECommerce.Application.Features.Products.DTOs
{
    public sealed record ProductDto(

        int Id,

        string Name,

        string Description,

        decimal Price,

        decimal? DiscountPrice,

        int StockQuantity,

        string SKU,

        string? ImageUrl,

        string Category,

        bool IsActive);
}
