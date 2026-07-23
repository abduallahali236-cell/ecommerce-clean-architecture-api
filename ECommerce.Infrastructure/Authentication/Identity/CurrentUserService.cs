using System.Security.Claims;
using ECommerce.Application.Common.Interfaces;
using Microsoft.AspNetCore.Http;

namespace ECommerce.Infrastructure.Authentication.Identity;

public sealed class CurrentUserService : ICurrentUserService
{
    private readonly IHttpContextAccessor _httpContextAccessor;

    public CurrentUserService(
        IHttpContextAccessor httpContextAccessor)
    {
        _httpContextAccessor = httpContextAccessor;
    }

    public int? UserId
    {
        get
        {
            var value = _httpContextAccessor
                .HttpContext?
                .User?
                .FindFirstValue(ClaimTypes.NameIdentifier);

            return int.TryParse(value, out var id)
                ? id
                : null;
        }
    }

    public string? Email =>
        _httpContextAccessor
            .HttpContext?
            .User?
            .FindFirstValue(ClaimTypes.Email);

    public bool IsAuthenticated =>
        _httpContextAccessor
            .HttpContext?
            .User?
            .Identity?
            .IsAuthenticated
        ?? false;
}