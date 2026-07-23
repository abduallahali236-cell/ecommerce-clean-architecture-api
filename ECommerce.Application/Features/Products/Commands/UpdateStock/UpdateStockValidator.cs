using System;
using System.Collections.Generic;
using System.Text;

namespace ECommerce.Application.Features.Products.Commands.UpdateStock
{
    public sealed class UpdateStockValidator
        : AbstractValidator<UpdateStockCommand>
    {
        public UpdateStockValidator()
        {
            RuleFor(x => x.ProductId)
                .GreaterThan(0);

            RuleFor(x => x.StockQuantity)
                .GreaterThanOrEqualTo(0);
        }
    }
}
