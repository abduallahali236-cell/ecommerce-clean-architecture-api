using ECommerce.Domain.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace ECommerce.Domain.Entities
{
    public class OrderItem : BaseEntity
    {
        public int OrderId { get; private set; }

        public int ProductId { get; private set; }

        public Order Order { get; private set; }
        public Product Product { get; private set; }

        public int Quantity { get; private set; }

        public decimal UnitPrice { get; private set; }
        public decimal TotalPrice => UnitPrice * Quantity;
        private OrderItem() { }

        public OrderItem(int productId, int quantity, decimal unitPrice)
        {
            ProductId = productId;
            Quantity = quantity;
            UnitPrice = unitPrice;
        }

    }
}
