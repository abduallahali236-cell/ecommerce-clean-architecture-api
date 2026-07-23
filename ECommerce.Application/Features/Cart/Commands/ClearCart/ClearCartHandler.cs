using ECommerce.Application.Common.Errors;
using System;
using System.Collections.Generic;
using System.Text;

namespace ECommerce.Application.Features.Cart.Commands.ClearCart
{
    public sealed class ClearCartHandler
        : IRequestHandler<ClearCartCommand, Result>
    {
        private readonly IApplicationDbContext _context;
        private readonly ICurrentUserService _currentUser;

        public ClearCartHandler(
            IApplicationDbContext context,
            ICurrentUserService currentUser)
        {
            _context = context;
            _currentUser = currentUser;
        }

        public async Task<Result> Handle(
            ClearCartCommand request,
            CancellationToken cancellationToken)
        {
            var cart = await _context.Carts
                .Include(x => x.Items)
                .FirstOrDefaultAsync(
                    x => x.UserId == _currentUser.UserId,
                    cancellationToken);

            if (cart is null)
                return Result.Failure(CartErrors.NotFound);

            cart.Clear();

            await _context.SaveChangesAsync(cancellationToken);

            return Result.Success();
        }
    }
}
