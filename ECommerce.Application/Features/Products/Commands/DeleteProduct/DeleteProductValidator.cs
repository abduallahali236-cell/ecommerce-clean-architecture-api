using ECommerce.Application.Features.Products.Commands.UpdateProduct;
using System;
using System.Collections.Generic;
using System.Text;

namespace ECommerce.Application.Features.Products.Commands.DeleteProduct
{

    public sealed class DeleteProductValidator
        : AbstractValidator<DeleteProductCommand>
    {
        public DeleteProductValidator()
        {
            RuleFor(x => x.Id)
                .GreaterThan(0);
        }
    }
}
