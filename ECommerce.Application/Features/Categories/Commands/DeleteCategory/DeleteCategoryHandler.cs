using ECommerce.Application.Common.Errors;
using System;
using System.Collections.Generic;
using System.Text;

namespace ECommerce.Application.Features.Categories.Commands.DeleteCategory
{
    public sealed class DeleteCategoryHandler
        : IRequestHandler<DeleteCategoryCommand, Result>
    {
        private readonly IApplicationDbContext _context;

        public DeleteCategoryHandler(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Result> Handle(
            DeleteCategoryCommand request,
            CancellationToken cancellationToken)
        {
            var category = await _context.Categories
                .FirstOrDefaultAsync(
                    x => x.Id == request.Id,
                    cancellationToken);

            if (category is null)
                return Result.Failure(CategoryErrors.NotFound);

            var hasProducts = await _context.Products
                .AnyAsync(
                    x => x.CategoryId == request.Id,
                    cancellationToken);

            if (hasProducts)
                return Result.Failure(CategoryErrors.HasProducts);

            _context.Categories.Remove(category);

            await _context.SaveChangesAsync(cancellationToken);

            return Result.Success();
        }
    }
}
