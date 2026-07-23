using ECommerce.Domain.Common;
using ECommerce.Domain.Exceptions;
using System;
using System.Collections.Generic;
using System.Text;

namespace ECommerce.Domain.Entities
{

    public sealed class Product : AuditableEntity
    {
        private Product() { } // Required by EF Core

        public Product(
            string name,
            string description,
            decimal price,
            decimal? discountPrice,
            int stockQuantity,
            string sku,
            string? imageUrl,
            int categoryId)
        {
            SetName(name);
            SetDescription(description);
            ChangePrice(price, discountPrice);

            StockQuantity = stockQuantity;
            SKU = sku;
            ImageUrl = imageUrl;
            CategoryId = categoryId;

            IsActive = true;
        }

        public string Name { get; private set; } = null!;

        public string Description { get; private set; } = null!;

        public decimal Price { get; private set; }

        public decimal? DiscountPrice { get; private set; }

        public int StockQuantity { get; private set; }

        public string SKU { get; private set; } = null!;

        public string? ImageUrl { get; private set; }

        public bool IsActive { get; private set; }

        public int CategoryId { get; private set; }

        public Category Category { get; private set; } = null!;

        public void Update(
            string name,
            string description,
            decimal price,
            decimal? discountPrice,
            int stockQuantity,
            string? imageUrl,
            int categoryId)
        {
            SetName(name);
            SetDescription(description);
            ChangePrice(price, discountPrice);

            StockQuantity = stockQuantity;
            ImageUrl = imageUrl;
            CategoryId = categoryId;
        }

        public void ChangePrice(
            decimal price,
            decimal? discountPrice)
        {
            if (price <= 0)
                throw new DomainException("Product price must be greater than zero.");

            if (discountPrice.HasValue &&
                discountPrice.Value >= price)
            {
                throw new DomainException(
                    "Discount price must be less than the original price.");
            }

            Price = price;
            DiscountPrice = discountPrice;
        }

        public void UpdateStock(int quantity)
        {
            if (quantity < 0)
                throw new DomainException(
                    "Stock quantity cannot be negative.");

            StockQuantity = quantity;
        }

        public void IncreaseStock(int quantity)
        {
            if (quantity <= 0)
                throw new DomainException(
                    "Quantity must be greater than zero.");

            StockQuantity += quantity;
        }

        public void DecreaseStock(int quantity)
        {
            if (quantity <= 0)
                throw new DomainException(
                    "Quantity must be greater than zero.");

            if (quantity > StockQuantity)
                throw new InsufficientStockException();

            StockQuantity -= quantity;
        }

        public void Activate()
        {
            IsActive = true;
        }

        public void Deactivate()
        {
            IsActive = false;
        }

        private void SetName(string name)
        {
            Name = name.Trim();
        }

        private void SetDescription(string description)
        {
            Description = description.Trim();
        }
    }
}
