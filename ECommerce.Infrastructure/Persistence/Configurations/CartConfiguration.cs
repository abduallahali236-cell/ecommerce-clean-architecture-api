using ECommerce.Domain.Entities;
using ECommerce.Infrastructure.Authentication.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ECommerce.Infrastructure.Persistence.Configurations;

public sealed class CartConfiguration : IEntityTypeConfiguration<Cart>
{
    public void Configure(EntityTypeBuilder<Cart> builder)
    {
        builder.ToTable("Carts");

        builder.HasKey(x => x.Id);

        builder.HasIndex(x => x.UserId)
               .IsUnique();

        builder.HasOne<ApplicationUser>()
               .WithOne(x => x.Cart)
               .HasForeignKey<Cart>(x => x.UserId)
               .OnDelete(DeleteBehavior.Cascade);

        builder.Navigation(x => x.Items)
               .UsePropertyAccessMode(PropertyAccessMode.Field);
    }
}