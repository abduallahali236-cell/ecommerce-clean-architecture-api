using MediatR;

namespace ECommerce.Application.Features.Products.Commands.CreateProduct
{
    public sealed record CreateProductCommand(

        string Name,

        string Description,

        decimal Price,

        decimal? DiscountPrice,

        int StockQuantity,

        string SKU,

        string? ImageUrl,

        int CategoryId)

        : IRequest<Result<int>>;
}
