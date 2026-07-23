using ECommerce.Domain.Common;
using ECommerce.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace ECommerce.Domain.Entities
{
    public class Payment : BaseEntity
    {
        public int OrderId { get; private set; }

        public decimal Amount { get; private set; }

        public Enums.enPayment.Method Method { get; private set; }

        public Enums.enPayment.Status Status { get; private set; }

        public Order Order { get; private set; }

        public string? TransactionId { get; private set; }

        private Payment() { }

        public Payment(int orderId, decimal amount, Enums.enPayment.Method method)
        {
            OrderId = orderId;
            Amount = amount;
            Method = method;
            Status = Enums.enPayment.Status.Pending;
        }

        public void MarkAsCompleted()
        {
            Status = Enums.enPayment.Status.Completed;
        }

        public void MarkAsFailed()
        {
            Status = Enums.enPayment.Status.Failed;
        }

        public void MarkAsRefunded()
        {
            Status = Enums.enPayment.Status.Refunded;
        }
    }
}
