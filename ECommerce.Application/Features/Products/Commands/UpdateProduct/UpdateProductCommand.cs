using MediatR;

namespace ECommerce.Application.Features.Products.Commands.UpdateProduct
{
    public sealed record UpdateProductCommand(
        int Id,
        string Name,
        string Description,
        decimal Price,
        decimal? DiscountPrice,
        int StockQuantity,
        string SKU,
        string? ImageUrl,
        int CategoryId)
        : IRequest<Result>;
}
