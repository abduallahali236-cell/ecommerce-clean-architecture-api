using ECommerce.Application.Common.Constants;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;

public static class RoleSeeder
{
    public static async Task SeedAsync(
        IServiceProvider services)
    {
        var roleManager =
            services.GetRequiredService<RoleManager<IdentityRole<int>>>();

        foreach (var role in new[]
        {
            Roles.Admin,
            Roles.Customer
        })
        {
            if (!await roleManager.RoleExistsAsync(role))
            {
                await roleManager.CreateAsync(
                    new IdentityRole<int>(role));
            }
        }
    }
}