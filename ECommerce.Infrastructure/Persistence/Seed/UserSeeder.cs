using ECommerce.Application.Common.Constants;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;

public static class UserSeeder
{
    public static async Task SeedAsync(
        IServiceProvider services)
    {
        var userManager =
            services.GetRequiredService<UserManager<ApplicationUser>>();

        var email = "admin@ecommerce.com";

        if (await userManager.FindByEmailAsync(email) != null)
            return;

        var admin = new ApplicationUser
        {
            FullName = "Administrator",
            Email = email,
            UserName = email
        };

        await userManager.CreateAsync(
            admin,
            "Admin123!");

        await userManager.AddToRoleAsync(
            admin,
            Roles.Admin);
    }
}