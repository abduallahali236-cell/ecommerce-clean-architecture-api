using System.Security.Cryptography;

public sealed class RefreshTokenGenerator
    : IRefreshTokenGenerator
{
    public string Generate()
    {
        return Convert.ToBase64String(
            RandomNumberGenerator.GetBytes(64));
    }
}