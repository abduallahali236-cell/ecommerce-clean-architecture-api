using ECommerce.Domain.Common;

namespace ECommerce.Domain.Entities;

public sealed class RefreshToken : AuditableEntity
{
    private RefreshToken() { }

    public RefreshToken(
        int userId,
        string token,
        DateTime expiresAt)
    {
        UserId = userId;
        Token = token;
        ExpiresAt = expiresAt;
    }

    public int UserId { get; private set; }

    public string Token { get; private set; } = null!;

    public DateTime ExpiresAt { get; private set; }

    public bool IsRevoked { get; private set; }

    public bool IsExpired => ExpiresAt <= DateTime.UtcNow;

    public bool IsActive => !IsRevoked && !IsExpired;

    public void Revoke()
    {
        IsRevoked = true;
    }
}