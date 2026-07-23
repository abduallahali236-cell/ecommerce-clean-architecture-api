using ECommerce.Application.Features.Orders.Commands.PlaceOrder;
using System;
using System.Collections.Generic;
using System.Text;

namespace ECommerce.Application.Features.Orders.Commands.PlaceOrder
{
    public sealed class PlaceOrderValidator
        : AbstractValidator<PlaceOrderCommand>
    {
        public PlaceOrderValidator()
        {
            RuleFor(x => x.FullName)
                .NotEmpty()
                .MaximumLength(100);

            RuleFor(x => x.PhoneNumber)
                .NotEmpty()
                .MaximumLength(20);

            RuleFor(x => x.City)
                .NotEmpty()
                .MaximumLength(100);

            RuleFor(x => x.AddressLine)
                .NotEmpty()
                .MaximumLength(300);
        }
    }
}
