using ECommerce.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ECommerce.Infrastructure.Persistence.Configurations;

public sealed class CartItemConfiguration : IEntityTypeConfiguration<CartItem>
{
    public void Configure(EntityTypeBuilder<CartItem> builder)
    {
        builder.ToTable("CartItems");

        builder.HasKey(x => x.Id);

        builder.Property(x => x.Quantity)
               .IsRequired();

        builder.HasIndex(x => new
        {
            x.CartId,
            x.ProductId
        })
        .IsUnique();

        builder.HasOne(x => x.Cart)
               .WithMany(x => x.Items)
               .HasForeignKey(x => x.CartId)
               .OnDelete(DeleteBehavior.Cascade);

        builder.HasOne(x => x.Product)
               .WithMany()
               .HasForeignKey(x => x.ProductId)
               .OnDelete(DeleteBehavior.Restrict);
    }
}