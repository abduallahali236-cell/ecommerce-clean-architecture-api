using ECommerce.Application.Features.Cart.Commands.UpdateCartItem;
using System;
using System.Collections.Generic;
using System.Text;

namespace ECommerce.Application.Features.Cart.Commands.UpdateCartItemQuantity
{
    public sealed class UpdateCartItemQuantityValidator
        : AbstractValidator<UpdateCartItemQuantityCommand>
    {
        public UpdateCartItemQuantityValidator()
        {
            RuleFor(x => x.ProductId)
                .GreaterThan(0);

            RuleFor(x => x.Quantity)
                .GreaterThan(0);
        }
    }
}
