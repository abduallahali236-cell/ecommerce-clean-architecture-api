using AutoMapper.QueryableExtensions;
using ECommerce.Application.Features.Products.DTOs;
using System;
using System.Collections.Generic;
using System.Text;

namespace ECommerce.Application.Features.Products.Queries.GetProducts
{
    public sealed class GetProductsHandler
        : IRequestHandler<GetProductsQuery, Result<PaginatedResult<ProductListItemDto>>>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;

        public GetProductsHandler(
            IApplicationDbContext context,
            IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<Result<PaginatedResult<ProductListItemDto>>> Handle(
            GetProductsQuery request,
            CancellationToken cancellationToken)
        {
            var query = _context.Products
                .AsNoTracking()
                .Include(x => x.Category)
                .Where(x => x.IsActive);

            if (request.CategoryId.HasValue)
            {
                query = query.Where(x => x.CategoryId == request.CategoryId.Value);
            }

            if (request.MinPrice.HasValue)
            {
                query = query.Where(x => x.Price >= request.MinPrice.Value);
            }

            if (request.MaxPrice.HasValue)
            {
                query = query.Where(x => x.Price <= request.MaxPrice.Value);
            }

            var totalCount = await query.CountAsync(cancellationToken);

            var products = await query
                .OrderBy(x => x.Name)
                .Skip((request.PageNumber - 1) * request.PageSize)
                .Take(request.PageSize)
                .ProjectTo<ProductListItemDto>(_mapper.ConfigurationProvider)
                .ToListAsync(cancellationToken);

            var result = new PaginatedResult<ProductListItemDto>
            {
                Items = products,
                PageNumber = request.PageNumber,
                PageSize = request.PageSize,
                TotalCount = totalCount
            };

            return Result<PaginatedResult<ProductListItemDto>>.Success(result);
        }
    }
}
