using ECommerce.Application.Common.Errors;
using ECommerce.Application.Features.Orders.Commands.PlaceOrder;
using System;
using System.Collections.Generic;
using System.Text;

namespace ECommerce.Application.Features.Orders.Commands.PlaceOrder
{
    public sealed class PlaceOrderHandler
        : IRequestHandler<PlaceOrderCommand, Result<int>>
    {
        private readonly IApplicationDbContext _context;
        private readonly ICurrentUserService _currentUser;

        public PlaceOrderHandler(
            IApplicationDbContext context,
            ICurrentUserService currentUser)
        {
            _context = context;
            _currentUser = currentUser;
        }

        public async Task<Result<int>> Handle(
            PlaceOrderCommand request,
            CancellationToken cancellationToken)
        {
            var cart = await _context.Carts
                .Include(x => x.Items)
                    .ThenInclude(x => x.Product)
                .FirstOrDefaultAsync(
                    x => x.UserId == _currentUser.UserId,
                    cancellationToken);

            if (cart is null)
                return Result<int>.Failure(CartErrors.NotFound);

            if (!cart.Items.Any())
                return Result<int>.Failure(OrderErrors.EmptyCart);

            var order = new Order(
                _currentUser.UserId!.Value,
                request.FullName,
                request.PhoneNumber,
                request.City,
                request.AddressLine);

            foreach (var item in cart.Items)
            {
                var product = item.Product;

                if (!product.IsActive)
                    return Result<int>.Failure(ProductErrors.NotFound);

                if (product.StockQuantity < item.Quantity)
                    return Result<int>.Failure(CartErrors.InsufficientStock);

                order.AddItem(
                    product.Id,
                    item.Quantity,
                    product.Price);

                product.DecreaseStock(item.Quantity);
            }

            _context.Orders.Add(order);

            cart.Clear();

            await _context.SaveChangesAsync(cancellationToken);

            return Result<int>.Success(order.Id);
        }
    }
}
