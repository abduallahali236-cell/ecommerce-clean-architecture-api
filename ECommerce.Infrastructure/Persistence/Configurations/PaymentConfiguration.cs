using ECommerce.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ECommerce.Infrastructure.Persistence.Configurations;

public sealed class PaymentConfiguration : IEntityTypeConfiguration<Payment>
{
    public void Configure(EntityTypeBuilder<Payment> builder)
    {
        builder.ToTable("Payments");

        builder.HasKey(x => x.Id);

        builder.Property(x => x.Amount)
               .HasPrecision(18, 2);

        builder.HasIndex(x => x.OrderId)
               .IsUnique();

        builder.Property(x => x.Method)
               .HasConversion<int>();

        builder.Property(x => x.Status)
               .HasConversion<int>();

        builder.HasOne(x => x.Order)
               .WithOne()
               .HasForeignKey<Payment>(x => x.OrderId)
               .OnDelete(DeleteBehavior.Restrict);
    }
}