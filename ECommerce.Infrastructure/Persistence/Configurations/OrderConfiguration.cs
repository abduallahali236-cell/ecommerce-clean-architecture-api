using ECommerce.Domain.Entities;
using ECommerce.Infrastructure.Authentication.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ECommerce.Infrastructure.Persistence.Configurations;

public sealed class OrderConfiguration : IEntityTypeConfiguration<Order>
{
    public void Configure(EntityTypeBuilder<Order> builder)
    {
        builder.ToTable("Orders");

        builder.HasKey(x => x.Id);

        builder.Property(x => x.FullName)
               .HasMaxLength(100);

        builder.Property(x => x.PhoneNumber)
               .HasMaxLength(20);

        builder.Property(x => x.City)
               .HasMaxLength(100);

        builder.Property(x => x.AddressLine)
               .HasMaxLength(300);

        builder.HasIndex(x => x.UserId);

        builder.HasIndex(x => x.Status);

        builder.HasOne<ApplicationUser>()
               .WithMany(x => x.Orders)
               .HasForeignKey(x => x.UserId)
               .OnDelete(DeleteBehavior.Restrict);

        builder.Navigation(x => x.Items)
               .UsePropertyAccessMode(PropertyAccessMode.Field);
    }
}