using System;
using System.Collections.Generic;
using System.Text;

namespace ECommerce.Application.Features.Products.Commands.DeleteProduct
{
    public sealed class DeleteProductHandler
        : IRequestHandler<DeleteProductCommand, Result>
    {
        private readonly IApplicationDbContext _context;

        public DeleteProductHandler(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Result> Handle(
            DeleteProductCommand request,
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

            product.Deactivate();

            await _context.SaveChangesAsync(cancellationToken);

            return Result.Success();
        }
    }
}
