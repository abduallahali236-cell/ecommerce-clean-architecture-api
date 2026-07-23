using ECommerce.Application.Features.Orders.Commands.ChangeOrderStatus;
using System;
using System.Collections.Generic;
using System.Text;

namespace ECommerce.Application.Features.Orders.Commands.UpdateOrderStatus
{
    public sealed class UpdateOrderStatusValidator
        : AbstractValidator<UpdateOrderStatusCommand>
    {
        public UpdateOrderStatusValidator()
        {
            RuleFor(x => x.OrderId)
                .GreaterThan(0);

            RuleFor(x => x.Status)
                .IsInEnum();
        }
    }
}
