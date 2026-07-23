using MediatR;
using ECommerce.Application.Features.Products.DTOs;

namespace ECommerce.Application.Features.Products.Queries.GetProductById
{
    // Placeholder query for getting a product by id
    public sealed record GetProductByIdQuery(int Id)
        : IRequest<Result<ProductDto>>;
}
