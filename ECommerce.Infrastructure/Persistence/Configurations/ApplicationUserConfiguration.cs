using ECommerce.Infrastructure.Authentication.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ECommerce.Infrastructure.Persistence.Configurations;

public sealed class ApplicationUserConfiguration
    : IEntityTypeConfiguration<ApplicationUser>
{
    public void Configure(EntityTypeBuilder<ApplicationUser> builder)
    {
        builder.Property(x => x.FullName)
               .HasMaxLength(100)
               .IsRequired();
    }
}