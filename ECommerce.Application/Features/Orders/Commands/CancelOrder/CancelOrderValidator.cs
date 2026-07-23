using ECommerce.Application.Features.Orders.Commands.DeleteOrder;
using System;
using System.Collections.Generic;
using System.Text;

namespace ECommerce.Application.Features.Orders.Commands.CancelOrder
{
    public sealed class CancelOrderValidator
        : AbstractValidator<CancelOrderCommand>
    {
        public CancelOrderValidator()
        {
            RuleFor(x => x.OrderId)
                .GreaterThan(0);
        }
    }
}
