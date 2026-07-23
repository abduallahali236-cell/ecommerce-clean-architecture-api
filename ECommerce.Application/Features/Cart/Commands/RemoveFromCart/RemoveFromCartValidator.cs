using System;
using System.Collections.Generic;
using System.Text;

namespace ECommerce.Application.Features.Cart.Commands.RemoveFromCart
{
    public sealed class RemoveFromCartValidator
        : AbstractValidator<RemoveFromCartCommand>
    {
        public RemoveFromCartValidator()
        {
            RuleFor(x => x.ProductId)
                .GreaterThan(0);
        }
    }
}
