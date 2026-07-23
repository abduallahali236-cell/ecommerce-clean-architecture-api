using System;
using System.Collections.Generic;
using System.Text;

namespace ECommerce.Application.Features.Products.Commands.CreateProduct
{
    public sealed class CreateProductValidator
      : AbstractValidator<CreateProductCommand>
    {
        public CreateProductValidator()
        {
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
                .NotEmpty();

            RuleFor(x => x.CategoryId)
                .GreaterThan(0);
        }
    }
}
