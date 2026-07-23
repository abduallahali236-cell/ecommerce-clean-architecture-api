using System;
using System.Collections.Generic;
using System.Text;

namespace ECommerce.Application.Features.Products.Commands.ChangePrice
{
    public sealed class ChangePriceValidator
        : AbstractValidator<ChangePriceCommand>
    {
        public ChangePriceValidator()
        {
            RuleFor(x => x.ProductId)
                .GreaterThan(0);

            RuleFor(x => x.Price)
                .GreaterThan(0);

            RuleFor(x => x.DiscountPrice)
                .LessThan(x => x.Price)
                .When(x => x.DiscountPrice.HasValue);
        }
    }
}
