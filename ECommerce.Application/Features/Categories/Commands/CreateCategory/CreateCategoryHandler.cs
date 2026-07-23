using ECommerce.Application.Common.Errors;
using System;
using System.Collections.Generic;
using System.Text;

namespace ECommerce.Application.Features.Categories.Commands.CreateCategory
{
    public sealed class CreateCategoryHandler
        : IRequestHandler<CreateCategoryCommand, Result<int>>
    {
        private readonly IApplicationDbContext _context;

        public CreateCategoryHandler(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Result<int>> Handle(
            CreateCategoryCommand request,
            CancellationToken cancellationToken)
        {
            var exists = await _context.Categories
                .AnyAsync(
                    x => x.Name == request.Name,
                    cancellationToken);

            if (exists)
            {
                return Result<int>.Failure(
                    CategoryErrors.DuplicateName);
            }

            var category = new Category(
                request.Name,
                request.Description);

            _context.Categories.Add(category);

            await _context.SaveChangesAsync(cancellationToken);

            return Result<int>.Success(category.Id);
        }
    }
}
