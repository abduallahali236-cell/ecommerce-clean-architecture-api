using ECommerce.Application.Common.Errors;
using ECommerce.Application.Features.Orders.Commands.ChangeOrderStatus;
using System;
using System.Collections.Generic;
using System.Text;

namespace ECommerce.Application.Features.Orders.Commands.UpdateOrderStatus
{
    public sealed class UpdateOrderStatusHandler
        : IRequestHandler<UpdateOrderStatusCommand, Result>
    {
        private readonly IApplicationDbContext _context;

        public UpdateOrderStatusHandler(
            IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Result> Handle(
            UpdateOrderStatusCommand request,
            CancellationToken cancellationToken)
        {
            var order = await _context.Orders
                .FirstOrDefaultAsync(
                    x => x.Id == request.OrderId,
                    cancellationToken);

            if (order is null)
                return Result.Failure(OrderErrors.NotFound);

            order.UpdateStatus(request.Status);

            await _context.SaveChangesAsync(cancellationToken);

            return Result.Success();
        }
    }
}
