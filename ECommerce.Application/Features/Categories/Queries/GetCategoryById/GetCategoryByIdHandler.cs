using ECommerce.Application.Common.Errors;
using ECommerce.Application.Features.Categories.DTOs;
using System;
using System.Collections.Generic;
using System.Text;

namespace ECommerce.Application.Features.Categories.Queries.GetCategoryById
{
    public sealed class GetCategoryByIdHandler
        : IRequestHandler<GetCategoryByIdQuery, Result<CategoryDto>>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;

        public GetCategoryByIdHandler(
            IApplicationDbContext context,
            IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<Result<CategoryDto>> Handle(
            GetCategoryByIdQuery request,
            CancellationToken cancellationToken)
        {
            var category = await _context.Categories
                .AsNoTracking()
                .FirstOrDefaultAsync(
                    x => x.Id == request.Id,
                    cancellationToken);

            if (category is null)
                return Result<CategoryDto>.Failure(CategoryErrors.NotFound);

            return Result<CategoryDto>.Success(
                _mapper.Map<CategoryDto>(category));
        }
    }
}
