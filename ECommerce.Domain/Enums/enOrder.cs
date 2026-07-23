using System;
using System.Collections.Generic;
using System.Text;

namespace ECommerce.Domain.Enums
{
    public class enOrder
    {
        public enum Status
        {
            Pending,
            Cancelled,
            Shipped,
            Delivered
        }

    }
}
