using ECommerce.Application.Common.Errors;
using System;
using System.Collections.Generic;
using System.Text;

namespace ECommerce.Application.Features.Products.Commands.CreateProduct
{
    public sealed class CreateProductHandler
      : IRequestHandler<CreateProductCommand, Result<int>>
    {
        private readonly IApplicationDbContext _context;

        public CreateProductHandler(
            IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Result<int>> Handle(
            CreateProductCommand request,
            CancellationToken cancellationToken)
        {
            var categoryExists = await _context.Categories
                .AnyAsync(
                    x => x.Id == request.CategoryId,
                    cancellationToken);

            if (!categoryExists)
            {
                return Result<int>.Failure(
                    new Error(
                        "Category.NotFound",
                        "Category was not found.",
                        ErrorType.NotFound));
            }

            var skuExists = await _context.Products
                .AnyAsync(
                    x => x.SKU == request.SKU,
                    cancellationToken);

            if (skuExists)
            {
                return Result<int>.Failure(
                    ProductErrors.DuplicateSku);
            }

            var product = new Product(
                request.Name,
                request.Description,
                request.Price,
                request.DiscountPrice,
                request.StockQuantity,
                request.SKU,
                request.ImageUrl,
                request.CategoryId);

            _context.Products.Add(product);

            await _context.SaveChangesAsync(
                cancellationToken);

            return Result<int>.Success(product.Id);
        }
    }
}
