using ECommerce.Application.Common.Errors;
using System;
using System.Collections.Generic;
using System.Text;

namespace ECommerce.Application.Features.Categories.Commands.UpdateCategory
{
    public sealed class UpdateCategoryHandler
        : IRequestHandler<UpdateCategoryCommand, Result>
    {
        private readonly IApplicationDbContext _context;

        public UpdateCategoryHandler(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Result> Handle(
            UpdateCategoryCommand request,
            CancellationToken cancellationToken)
        {
            var category = await _context.Categories
                .FirstOrDefaultAsync(
                    x => x.Id == request.Id,
                    cancellationToken);

            if (category is null)
                return Result.Failure(CategoryErrors.NotFound);

            var exists = await _context.Categories
                .AnyAsync(x =>
                    x.Name == request.Name &&
                    x.Id != request.Id,
                    cancellationToken);

            if (exists)
                return Result.Failure(CategoryErrors.DuplicateName);

            category.Update(
                request.Name,
                request.Description);

            await _context.SaveChangesAsync(cancellationToken);

            return Result.Success();
        }
    }
}
