using MediatR;

namespace ECommerce.Application.Features.Products.Commands.UpdateStock
{
    // Placeholder command for updating product stock
    public sealed record UpdateStockCommand(
        int ProductId,
        int StockQuantity)
        : IRequest<Result>;
}
