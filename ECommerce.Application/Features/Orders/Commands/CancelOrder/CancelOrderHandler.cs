using ECommerce.Application.Common.Errors;
using ECommerce.Application.Features.Orders.Commands.DeleteOrder;
using ECommerce.Domain.Exceptions;
using System;
using System.Collections.Generic;
using System.Text;

namespace ECommerce.Application.Features.Orders.Commands.CancelOrder
{
    public sealed class CancelOrderHandler
        : IRequestHandler<CancelOrderCommand, Result>
    {
        private readonly IApplicationDbContext _context;
        private readonly ICurrentUserService _currentUser;

        public CancelOrderHandler(
            IApplicationDbContext context,
            ICurrentUserService currentUser)
        {
            _context = context;
            _currentUser = currentUser;
        }

        public async Task<Result> Handle(
            CancelOrderCommand request,
            CancellationToken cancellationToken)
        {
            var order = await _context.Orders
                .Include(x => x.Items)
                .FirstOrDefaultAsync(
                    x => x.Id == request.OrderId &&
                         x.UserId == _currentUser.UserId,
                    cancellationToken);

            if (order is null)
                return Result.Failure(OrderErrors.NotFound);

            try
            {
                order.Cancel();
            }
            catch (DomainException)
            {
                return Result.Failure(OrderErrors.CannotCancel);
            }
            var productIds = order.Items
                .Select(x => x.ProductId)
                .ToList();

            var products = await _context.Products
                .Where(x => productIds.Contains(x.Id))
                .ToListAsync(cancellationToken);

            foreach (var item in order.Items)
            {
                var product = products.First(x => x.Id == item.ProductId);

                product.IncreaseStock(item.Quantity);
            }

            await _context.SaveChangesAsync(cancellationToken);

            return Result.Success();
        }
    }
}
