using System;
using System.Collections.Generic;
using System.Text;

namespace ECommerce.Application.Common.Errors
{
    public static class CartErrors
    {
        public static readonly Error NotFound =
            new(
                "Cart.NotFound",
                "Shopping cart was not found.",
                ErrorType.NotFound);

        public static readonly Error ProductNotFound =
            new(
                "Cart.ProductNotFound",
                "Product was not found.",
                ErrorType.NotFound);

        public static readonly Error InsufficientStock =
            new(
                "Cart.InsufficientStock",
                "Insufficient stock.",
                ErrorType.Conflict);
    }
}
