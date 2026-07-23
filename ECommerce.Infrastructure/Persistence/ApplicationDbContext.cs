using ECommerce.Application.Common.Interfaces;
using ECommerce.Domain.Entities;
using ECommerce.Infrastructure.Authentication.Identity;
using ECommerce.Infrastructure.Persistence.Interceptors;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace ECommerce.Infrastructure.Persistence;

public sealed class ApplicationDbContext
    : IdentityDbContext<ApplicationUser, IdentityRole<int>, int>,
      IApplicationDbContext
{
    private readonly AuditableEntityInterceptor _auditableInterceptor;


    public ApplicationDbContext(
        DbContextOptions<ApplicationDbContext> options,
        AuditableEntityInterceptor auditableInterceptor)
        : base(options)
    {
        _auditableInterceptor = auditableInterceptor;
    }

    public DbSet<Product> Products => Set<Product>();

    public DbSet<Category> Categories => Set<Category>();

    public DbSet<Cart> Carts => Set<Cart>();

    public DbSet<CartItem> CartItems => Set<CartItem>();

    public DbSet<Order> Orders => Set<Order>();

    public DbSet<OrderItem> OrderItems => Set<OrderItem>();

    public DbSet<Payment> Payments => Set<Payment>();

    public DbSet<RefreshToken> RefreshTokens => Set<RefreshToken>();

    protected override void OnConfiguring(
        DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.AddInterceptors(_auditableInterceptor);
    }

    protected override void OnModelCreating(
        ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        builder.ApplyConfigurationsFromAssembly(
            typeof(ApplicationDbContext).Assembly);
    }
}