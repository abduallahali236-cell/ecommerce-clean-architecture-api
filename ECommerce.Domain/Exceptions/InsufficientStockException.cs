using System;
using System.Collections.Generic;
using System.Text;

namespace ECommerce.Domain.Exceptions
{
    public class InsufficientStockException : DomainException
    {
        public InsufficientStockException()
            : base("Insufficient stock.")
        {
        }
    }
}
