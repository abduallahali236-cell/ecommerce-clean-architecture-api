using ECommerce.Domain.Common;
using ECommerce.Domain.Exceptions;
using System;
using System.Collections.Generic;
using System.Text;

namespace ECommerce.Domain.Entities
{
    public sealed class CartItem : BaseEntity
    {
        private CartItem() { }

        internal CartItem(
            int productId,
            int quantity,
            decimal unitPrice)
        {
            ProductId = productId;
            UnitPrice = unitPrice;

            UpdateQuantity(quantity);
        }

        public int CartId { get; private set; }

        public Cart Cart { get; private set; } = null!;

        public int ProductId { get; private set; }

        public Product Product { get; private set; } = null!;

        public int Quantity { get; private set; }

        public decimal UnitPrice { get; private set; }

        public decimal TotalPrice => UnitPrice * Quantity;

        public void UpdateQuantity(int quantity)
        {
            if (quantity <= 0)
                throw new DomainException(
                    "Quantity must be greater than zero.");

            Quantity = quantity;
        }

        public void ChangeUnitPrice(decimal unitPrice)
        {
            if (unitPrice <= 0)
                throw new DomainException(
                    "Price must be greater than zero.");

            UnitPrice = unitPrice;
        }
    }
}
