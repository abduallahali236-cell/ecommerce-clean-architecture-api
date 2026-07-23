using System;
using System.Collections.Generic;
using System.Text;

namespace ECommerce.Application.Features.Cart.Commands.AddToCart
{
    public sealed class AddToCartValidator
        : AbstractValidator<AddToCartCommand>
    {
        public AddToCartValidator()
        {
            RuleFor(x => x.ProductId)
                .GreaterThan(0);

            RuleFor(x => x.Quantity)
                .GreaterThan(0);
        }
    }
}
