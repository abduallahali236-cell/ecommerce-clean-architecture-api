using System;
using System.Collections.Generic;
using System.Text;

namespace ECommerce.Application.Features.Products.Commands.ChangePrice
{
    public sealed class ChangePriceHandler
        : IRequestHandler<ChangePriceCommand, Result>
    {
        private readonly IApplicationDbContext _context;

        public ChangePriceHandler(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Result> Handle(
            ChangePriceCommand request,
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

            product.ChangePrice(
                request.Price,
                request.DiscountPrice);

            await _context.SaveChangesAsync(cancellationToken);

            return Result.Success();
        }
    }
}
