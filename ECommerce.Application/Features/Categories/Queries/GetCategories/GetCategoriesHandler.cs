using AutoMapper.QueryableExtensions;
using ECommerce.Application.Features.Categories.DTOs;
using System;
using System.Collections.Generic;
using System.Text;

namespace ECommerce.Application.Features.Categories.Queries.GetCategories
{
    public sealed class GetCategoriesHandler
        : IRequestHandler<GetCategoriesQuery,
            Result<PaginatedResult<CategoryListItemDto>>>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;

        public GetCategoriesHandler(
            IApplicationDbContext context,
            IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<Result<PaginatedResult<CategoryListItemDto>>> Handle(
            GetCategoriesQuery request,
            CancellationToken cancellationToken)
        {
            var query = _context.Categories
                .AsNoTracking()
                .OrderBy(x => x.Name);

            var totalCount = await query.CountAsync(cancellationToken);

            var categories = await query
                .Skip((request.PageNumber - 1) * request.PageSize)
                .Take(request.PageSize)
                .ProjectTo<CategoryListItemDto>(_mapper.ConfigurationProvider)
                .ToListAsync(cancellationToken);

            var result = new PaginatedResult<CategoryListItemDto>
            {
                Items = categories,
                PageNumber = request.PageNumber,
                PageSize = request.PageSize,
                TotalCount = totalCount
            };

            return Result<PaginatedResult<CategoryListItemDto>>
                .Success(result);
        }
    }
}
