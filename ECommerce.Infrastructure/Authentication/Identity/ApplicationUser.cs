using ECommerce.Domain.Entities;
using Microsoft.AspNetCore.Identity;

public sealed class ApplicationUser : IdentityUser<int>
{
    public string FullName { get; set; } = string.Empty;

    public Cart? Cart { get; set; }

    public ICollection<Order> Orders { get; set; }
        = [];

    public ICollection<RefreshToken> RefreshTokens { get; set; }
        = [];
}