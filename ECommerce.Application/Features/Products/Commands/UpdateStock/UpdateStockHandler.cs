using System;
using System.Collections.Generic;
using System.Text;

namespace ECommerce.Application.Features.Products.Commands.UpdateStock
{
    public sealed class UpdateStockHandler
        : IRequestHandler<UpdateStockCommand, Result>
    {
        private readonly IApplicationDbContext _context;

        public UpdateStockHandler(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Result> Handle(
            UpdateStockCommand request,
            CancellationToken cancellationToken)
        {
            var product = await _context.Products
                .FirstOrDefaultAsync(
                    p => p.Id == request.ProductId && p.IsActive,
                    cancellationToken);

            if (product is null)
            {
                return Result.Failure(
                    new Error(
                        "Product.NotFound",
                        "The product was not found.",
                        ErrorType.NotFound));
            }

            product.UpdateStock(request.StockQuantity);

            await _context.SaveChangesAsync(cancellationToken);

            return Result.Success();
        }
    }
}
