using ECommerce.Domain.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace ECommerce.Domain.Entities
{
    public class Category : AuditableEntity
    {
        public string Name { get; private set; }

        public string? Description { get; private set; }

        public string? ImageUrl { get; private set; }

        public ICollection<Product> Products { get; private set; }
            = new List<Product>();

        private Category() { }

        public Category(string name, string? description)
        {
            Name = name;
            Description = description;
        }

        public void Update(
            string name,
            string? description)
        {
            Name = name.Trim();
            Description = description?.Trim();
        }
    }
}
