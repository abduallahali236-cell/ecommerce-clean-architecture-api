using ECommerce.Application.Common.Errors;
using ECommerce.Application.Features.Cart.DTOs;
using System;
using System.Collections.Generic;
using System.Text;

namespace ECommerce.Application.Features.Cart.Queries.GetCart
{
    public sealed class GetCartHandler
        : IRequestHandler<GetCartQuery, Result<CartDto>>
    {
        private readonly IApplicationDbContext _context;
        private readonly ICurrentUserService _currentUser;
        private readonly IMapper _mapper;

        public GetCartHandler(
            IApplicationDbContext context,
            ICurrentUserService currentUser,
            IMapper mapper)
        {
            _context = context;
            _currentUser = currentUser;
            _mapper = mapper;
        }

        public async Task<Result<CartDto>> Handle(
            GetCartQuery request,
            CancellationToken cancellationToken)
        {
            var cart = await _context.Carts

                .Include(x => x.Items)
                    .ThenInclude(x => x.Product)

                .AsNoTracking()

                .FirstOrDefaultAsync(
                    x => x.Id == _currentUser.UserId,
                    cancellationToken);

            if (cart is null)
                return Result<CartDto>.Failure(
                    CartErrors.NotFound);

            return Result<CartDto>.Success(
                _mapper.Map<CartDto>(cart));
        }
    }
}
