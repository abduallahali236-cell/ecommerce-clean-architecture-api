using ECommerce.Domain.Common;
using ECommerce.Domain.Enums;
using ECommerce.Domain.Exceptions;
using System;
using System.Collections.Generic;
using System.Text;

namespace ECommerce.Domain.Entities
{
    public sealed class Order : AuditableEntity
    {
        private readonly List<OrderItem> _items = new();

        private Order() { }

        public Order(
            int userId,
            string fullName,
            string phoneNumber,
            string city,
            string addressLine)
        {
            UserId = userId;
            FullName = fullName;
            PhoneNumber = phoneNumber;
            City = city;
            AddressLine = addressLine;

            Status = enOrder.Status.Pending;
        }

        public int UserId { get; private set; }

        public string FullName { get; private set; } = null!;

        public string PhoneNumber { get; private set; } = null!;

        public string City { get; private set; } = null!;

        public string AddressLine { get; private set; } = null!;

        public enOrder.Status Status { get; private set; }

        public IReadOnlyCollection<OrderItem> Items => _items.AsReadOnly();

        public decimal TotalAmount => _items.Sum(x => x.TotalPrice);

        public void AddItem(
            int productId,
            int quantity,
            decimal unitPrice)
        {
            _items.Add(new OrderItem(
                productId,
                quantity,
                unitPrice));
        }

        public void Cancel()
        {
            if (Status is enOrder.Status.Shipped
                or enOrder.Status.Delivered)
            {
                throw new DomainException(
                    "This order cannot be cancelled.");
            }

            Status = enOrder.Status.Cancelled;
        }

        public void UpdateStatus(enOrder.Status status)
        {
            Status = status;
        }
    }
}
