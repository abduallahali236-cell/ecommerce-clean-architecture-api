using System;
using System.Collections.Generic;
using System.Text;

namespace ECommerce.Application.Common.Interfaces
{
    public interface IApplicationDbContext
    {
        DbSet<Product> Products { get; }

        DbSet<Category> Categories { get; }

        DbSet<Cart> Carts { get; }

        DbSet<CartItem> CartItems { get; }

        DbSet<Order> Orders { get; }

        DbSet<OrderItem> OrderItems { get; }

        DbSet<Payment> Payments { get; }

        DbSet<RefreshToken> RefreshTokens { get; }

        Task<int> SaveChangesAsync(
            CancellationToken cancellationToken = default);
    }
}
