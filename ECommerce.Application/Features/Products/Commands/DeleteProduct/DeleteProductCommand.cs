using MediatR;

namespace ECommerce.Application.Features.Products.Commands.DeleteProduct
{
    // Placeholder command for deleting a product

    public sealed record DeleteProductCommand(int Id)
        : IRequest<Result>;
}
