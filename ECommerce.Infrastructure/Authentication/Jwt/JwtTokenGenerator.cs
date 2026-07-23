using ECommerce.Infrastructure.Authentication.Jwt;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

public sealed class JwtTokenGenerator : IJwtTokenGenerator
{
    private readonly JwtOptions _options;

    public JwtTokenGenerator(
        IOptions<JwtOptions> options)
    {
        _options = options.Value;
    }

    public async Task<JwtTokenResult> GenerateAsync(ApplicationUser user)
    {
        var claims = new[]
        {
            new Claim(
                ClaimTypes.NameIdentifier,
                user.Id.ToString()),

            new Claim(
                ClaimTypes.Email,
                user.Email!),

            new Claim(
                ClaimTypes.Name,
                user.FullName)
        };

        var key = new SymmetricSecurityKey(
            Encoding.UTF8.GetBytes(_options.SecretKey));

        var credentials =
            new SigningCredentials(
                key,
                SecurityAlgorithms.HmacSha256);

        var token = new JwtSecurityToken(
            issuer: _options.Issuer,
            audience: _options.Audience,
            claims: claims,
            expires: DateTime.UtcNow.AddMinutes(
                _options.ExpirationMinutes),
            signingCredentials: credentials);

        return new JwtTokenResult(
            AccessToken: new JwtSecurityTokenHandler().WriteToken(token),
            ExpiresAt: DateTime.UtcNow.AddMinutes(_options.ExpirationMinutes)
        );
    }
}