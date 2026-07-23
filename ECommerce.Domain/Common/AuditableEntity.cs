using System;
using System.Collections.Generic;
using System.Text;

namespace ECommerce.Domain.Common
{
    public abstract class AuditableEntity : BaseEntity
    {
        public DateTime CreatedAt { get; private set; }

        public DateTime? UpdatedAt { get; private set; }

        public void SetCreated(DateTime createdAt)
        {
            CreatedAt = createdAt;
        }

        public void SetUpdated(DateTime updatedAt)
        {
            UpdatedAt = updatedAt;
        }
    }
}
