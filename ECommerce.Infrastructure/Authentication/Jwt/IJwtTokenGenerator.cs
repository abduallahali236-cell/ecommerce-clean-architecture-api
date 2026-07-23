using ECommerce.Infrastructure.Authentication.Identity;

namespace ECommerce.Infrastructure.Authentication.Jwt;

public interface IJwtTokenGenerator
{
    Task<JwtTokenResult> GenerateAsync(ApplicationUser user);
}