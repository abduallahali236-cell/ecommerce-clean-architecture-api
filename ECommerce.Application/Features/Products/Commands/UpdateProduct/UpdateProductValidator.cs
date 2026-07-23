using System;
using System.Collections.Generic;
using System.Text;

namespace ECommerce.Application.Features.Products.Commands.UpdateProduct
{
    public sealed class UpdateProductValidator
        : AbstractValidator<UpdateProductCommand>
    {
        public UpdateProductValidator()
        {
            RuleFor(x => x.Id)
                .GreaterThan(0);

            RuleFor(x => x.Name)
                .NotEmpty()
                .MaximumLength(200);

            RuleFor(x => x.Description)
                .NotEmpty();

            RuleFor(x => x.Price)
                .GreaterThan(0);

            RuleFor(x => x.StockQuantity)
                .GreaterThanOrEqualTo(0);

            RuleFor(x => x.SKU)
                .NotEmpty()
                .MaximumLength(50);

            RuleFor(x => x.CategoryId)
                .GreaterThan(0);
        }
    }
}
