using ECommerce.Domain.Common;
using ECommerce.Domain.Exceptions;
using System;
using System.Collections.Generic;
using System.Text;

namespace ECommerce.Domain.Entities
{
    public sealed class Cart : AuditableEntity
    {
        private readonly List<CartItem> _items = new();

        private Cart() { }

        public Cart(int userId)
        {
            UserId = userId;
        }

        public int UserId { get; private set; }

        public IReadOnlyCollection<CartItem> Items => _items.AsReadOnly();

        public decimal TotalPrice => _items.Sum(x => x.TotalPrice);

        public int TotalItems => _items.Sum(x => x.Quantity);

        public void AddItem(
            int productId,
            int quantity,
            decimal unitPrice)
        {
            if (quantity <= 0)
                throw new DomainException("Quantity must be greater than zero.");

            var item = _items.FirstOrDefault(x => x.ProductId == productId);

            if (item is null)
            {
                _items.Add(new CartItem(
                    productId,
                    quantity,
                    unitPrice));
            }
            else
            {
                item.UpdateQuantity(item.Quantity + quantity);
            }
        }

        public void RemoveItem(int productId)
        {
            var item = _items.FirstOrDefault(x => x.ProductId == productId);

            if (item is null)
                throw new DomainException("Cart item was not found.");

            _items.Remove(item);
        }

        public void Clear()
        {
            _items.Clear();
        }
    }
}
