using ECommerce.Application.Common.Errors;
using ECommerce.Domain.Exceptions;
using System;
using System.Collections.Generic;
using System.Text;

namespace ECommerce.Application.Features.Cart.Commands.RemoveFromCart
{
    public sealed class RemoveFromCartHandler
        : IRequestHandler<RemoveFromCartCommand, Result>
    {
        private readonly IApplicationDbContext _context;
        private readonly ICurrentUserService _currentUser;

        public RemoveFromCartHandler(
            IApplicationDbContext context,
            ICurrentUserService currentUser)
        {
            _context = context;
            _currentUser = currentUser;
        }

        public async Task<Result> Handle(
            RemoveFromCartCommand request,
            CancellationToken cancellationToken)
        {
            var cart = await _context.Carts
                .Include(x => x.Items)
                .FirstOrDefaultAsync(
                    x => x.UserId == _currentUser.UserId,
                    cancellationToken);

            if (cart is null)
                return Result.Failure(CartErrors.NotFound);

            try
            {
                cart.RemoveItem(request.ProductId);
            }
            catch (DomainException)
            {
                return Result.Failure(CartErrors.ProductNotFound);
            }

            await _context.SaveChangesAsync(cancellationToken);

            return Result.Success();
        }
    }
}
