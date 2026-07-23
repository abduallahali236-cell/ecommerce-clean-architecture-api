using System;
using System.Collections.Generic;
using System.Text;

namespace ECommerce.Application.Features.Products.Commands.UpdateProduct
{
    public sealed class UpdateProductHandler
        : IRequestHandler<UpdateProductCommand, Result>
    {
        private readonly IApplicationDbContext _context;

        public UpdateProductHandler(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Result> Handle(
            UpdateProductCommand request,
            CancellationToken cancellationToken)
        {
            var product = await _context.Products
                .FirstOrDefaultAsync(
                    p => p.Id == request.Id,
                    cancellationToken);

            if (product is null)
            {
                return Result.Failure(
                    new Error(
                        "Product.NotFound",
                        "The product was not found.",
                        ErrorType.NotFound));
            }

            var categoryExists = await _context.Categories
                .AnyAsync(
                    c => c.Id == request.CategoryId,
                    cancellationToken);

            if (!categoryExists)
            {
                return Result.Failure(
                    new Error(
                        "Category.NotFound",
                        "The category was not found.",
                        ErrorType.NotFound));
            }

            var skuExists = await _context.Products
                .AnyAsync(
                    p => p.SKU == request.SKU && p.Id != request.Id,
                    cancellationToken);

            if (skuExists)
            {
                return Result.Failure(
                    new Error(
                        "Product.DuplicateSku",
                        "SKU already exists.",
                        ErrorType.Conflict));
            }

            product.Update(
                request.Name,
                request.Description,
                request.Price,
                request.DiscountPrice,
                request.StockQuantity,
                request.ImageUrl,
                request.CategoryId);

            await _context.SaveChangesAsync(cancellationToken);

            return Result.Success();
        }
    }
}
