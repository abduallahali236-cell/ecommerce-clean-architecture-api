using ECommerce.Application.Common.Errors;
using System;
using System.Collections.Generic;
using System.Text;

namespace ECommerce.Application.Features.Cart.Commands.AddToCart
{
    public sealed class AddToCartHandler
        : IRequestHandler<AddToCartCommand, Result>
    {
        private readonly IApplicationDbContext _context;
        private readonly ICurrentUserService _currentUser;

        public AddToCartHandler(
            IApplicationDbContext context,
            ICurrentUserService currentUser)
        {
            _context = context;
            _currentUser = currentUser;
        }

        public async Task<Result> Handle(
            AddToCartCommand request,
            CancellationToken cancellationToken)
        {
            var product = await _context.Products
                .FirstOrDefaultAsync(
                    x => x.Id == request.ProductId &&
                         x.IsActive,
                    cancellationToken);

            if (product is null)
                return Result.Failure(
                    CartErrors.ProductNotFound);

            if (product.StockQuantity < request.Quantity)
                return Result.Failure(
                    CartErrors.InsufficientStock);

            var cart = await _context.Carts

                .Include(x => x.Items)

                .FirstOrDefaultAsync(
                    x => x.Id == _currentUser.UserId,
                    cancellationToken);

            if (cart is null)
            {
                cart = new Domain.Entities.Cart(_currentUser.UserId!.Value);

                _context.Carts.Add(cart);
            }

            var item = cart.Items
                .FirstOrDefault(x => x.ProductId == request.ProductId);

            if (item is null)
            {
                cart.AddItem(
                    request.ProductId,
                    request.Quantity,
                    product.Price);
            }
            else
            {
                item.UpdateQuantity(
                    item.Quantity + request.Quantity);
            }

            await _context.SaveChangesAsync(cancellationToken);

            return Result.Success();
        }
    }
}
