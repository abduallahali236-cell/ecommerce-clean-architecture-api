using System;
using System.Collections.Generic;
using System.Text;

namespace ECommerce.Application.Common.Errors
{
    public static class OrderErrors
    {
        public static readonly Error NotFound =
            new(
                "Order.NotFound",
                "Order was not found.",
                ErrorType.NotFound);

        public static readonly Error EmptyCart =
            new(
                "Order.EmptyCart",
                "Your shopping cart is empty.",
                ErrorType.Validation);

        public static readonly Error InvalidStatus =
            new(
                "Order.InvalidStatus",
                "Invalid order status.",
                ErrorType.Validation);

        public static readonly Error CannotCancel =
            new(
                "Order.CannotCancel",
                "This order cannot be cancelled.",
                ErrorType.Conflict);
    }
}
