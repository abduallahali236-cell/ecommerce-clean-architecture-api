using System;
using System.Collections.Generic;
using System.Text;

namespace ECommerce.Domain.Enums
{
    public class enPayment
    {
        public enum Method
        {
            CreditCard,
            PayPal,
            BankTransfer
        }
        public enum Status
        {
            Pending,
            Completed,
            Failed,
            Refunded
        }

    }
}
