using ECommerce.Application.Common.Errors;
using ECommerce.Application.Features.Cart.Commands.UpdateCartItem;
using System;
using System.Collections.Generic;
using System.Text;

namespace ECommerce.Application.Features.Cart.Commands.UpdateCartItemQuantity
{
    public sealed class UpdateCartItemQuantityHandler
        : IRequestHandler<UpdateCartItemQuantityCommand, Result>
    {
        private readonly IApplicationDbContext _context;
        private readonly ICurrentUserService _currentUser;

        public UpdateCartItemQuantityHandler(
            IApplicationDbContext context,
            ICurrentUserService currentUser)
        {
            _context = context;
            _currentUser = currentUser;
        }

        public async Task<Result> Handle(
            UpdateCartItemQuantityCommand request,
            CancellationToken cancellationToken)
        {
            var cart = await _context.Carts
                .Include(x => x.Items)
                .FirstOrDefaultAsync(
                    x => x.UserId == _currentUser.UserId,
                    cancellationToken);

            if (cart is null)
                return Result.Failure(CartErrors.NotFound);

            var item = cart.Items
                .FirstOrDefault(x => x.ProductId == request.ProductId);

            if (item is null)
                return Result.Failure(CartErrors.ProductNotFound);

            var product = await _context.Products
                .FirstOrDefaultAsync(
                    x => x.Id == request.ProductId && x.IsActive,
                    cancellationToken);

            if (product is null)
                return Result.Failure(CartErrors.ProductNotFound);

            if (product.StockQuantity < request.Quantity)
                return Result.Failure(CartErrors.InsufficientStock);

            item.UpdateQuantity(request.Quantity);

            await _context.SaveChangesAsync(cancellationToken);

            return Result.Success();
        }
    }
}
