using ECommerce.Application.Common.Interfaces;
using ECommerce.Infrastructure.Authentication.Identity;
using ECommerce.Infrastructure.Authentication.Jwt;
using ECommerce.Infrastructure.Persistence;
using ECommerce.Infrastructure.Persistence.Interceptors;
using ECommerce.Infrastructure.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace ECommerce.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        services.Configure<JwtOptions>(
            configuration.GetSection(JwtOptions.SectionName));

        services.AddDbContext<ApplicationDbContext>(options =>
        {
            options.UseSqlServer(
                configuration.GetConnectionString("DefaultConnection"));
        });

        services.AddScoped<ECommerce.Application.Common.Interfaces.IApplicationDbContext>(
            sp => sp.GetRequiredService<ApplicationDbContext>());

        services.AddSingleton<IDateTimeProvider, DateTimeProvider>();

        services.AddScoped<AuditableEntityInterceptor>();

        services.AddScoped<IIdentityService, IdentityService>();

        services.AddScoped<ICurrentUserService, CurrentUserService>();

        services.AddScoped<IJwtTokenGenerator, JwtTokenGenerator>();

        services.AddSingleton<IRefreshTokenGenerator, RefreshTokenGenerator>();

        services.AddHttpContextAccessor();

        // Payment service removed

        services.AddIdentity<ApplicationUser, IdentityRole<int>>(options =>
        {
            options.Password.RequiredLength = 8;
            options.Password.RequireDigit = true;
            options.Password.RequireUppercase = true;
            options.Password.RequireLowercase = true;
            options.Password.RequireNonAlphanumeric = false;

            options.User.RequireUniqueEmail = true;
        })
        .AddEntityFrameworkStores<ApplicationDbContext>()
        .AddDefaultTokenProviders();

        services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(options =>
            {
                var jwt = configuration
                    .GetSection(JwtOptions.SectionName)
                    .Get<JwtOptions>()!;

                options.TokenValidationParameters =
                    new TokenValidationParameters
                    {
                        ValidateIssuer = true,
                        ValidateAudience = true,
                        ValidateLifetime = true,
                        ValidateIssuerSigningKey = true,

                        ValidIssuer = jwt.Issuer,
                        ValidAudience = jwt.Audience,

                        IssuerSigningKey =
                            new SymmetricSecurityKey(
                                Encoding.UTF8.GetBytes(jwt.SecretKey)),

                        ClockSkew = TimeSpan.Zero
                    };
            });

        services.AddAuthorization();

        return services;
    }
}