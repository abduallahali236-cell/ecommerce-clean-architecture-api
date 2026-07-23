using System;
using System.Collections.Generic;
using System.Text;

namespace ECommerce.Application.Common.Errors
{
    public static class ProductErrors
    {
        public static readonly Error NotFound =
            new(
                "Product.NotFound",
                "The requested product was not found.",
                ErrorType.NotFound);

        public static readonly Error DuplicateSku =
            new(
                "Product.DuplicateSku",
                "A product with the same SKU already exists.",
                ErrorType.Conflict);
    }
}
