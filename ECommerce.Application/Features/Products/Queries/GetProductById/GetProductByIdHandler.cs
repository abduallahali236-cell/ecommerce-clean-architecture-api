using ECommerce.Application.Features.Products.DTOs;
using System;
using System.Collections.Generic;
using System.Text;

namespace ECommerce.Application.Features.Products.Queries.GetProductById
{
    public sealed class GetProductByIdHandler
        : IRequestHandler<GetProductByIdQuery, Result<ProductDto>>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;

        public GetProductByIdHandler(
            IApplicationDbContext context,
            IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<Result<ProductDto>> Handle(
            GetProductByIdQuery request,
            CancellationToken cancellationToken)
        {
            var product = await _context.Products
                .AsNoTracking()
                .Include(p => p.Category)
                .FirstOrDefaultAsync(
                    p => p.Id == request.Id && p.IsActive,
                    cancellationToken);

            if (product is null)
            {
                return Result<ProductDto>.Failure(
                    new Error(
                        "Product.NotFound",
                        "The product was not found.",
                        ErrorType.NotFound));
            }

            return Result<ProductDto>.Success(
                _mapper.Map<ProductDto>(product));
        }
    }
}
