using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ECommerce.Domain.Entities;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;

namespace ECommerce.Infrastructure.Persistence
{
    public static class DatabaseSeeder
    {
        public static async Task SeedAsync(IServiceProvider services)
        {
            using var scope = services.CreateScope();
            var provider = scope.ServiceProvider;

            var context = provider.GetRequiredService<ApplicationDbContext>();

            // apply pending migrations (optional but recommended)
            if ((await context.Database.GetPendingMigrationsAsync()).Any())
            {
                await context.Database.MigrateAsync();
            }

            // Seed Categories + Products only if empty
            if (!context.Categories.Any())
            {
                var categories = new List<Category>
                {
                    new Category("Electronics", "Phones, laptops and accessories"),
                    new Category("Clothing", "Men's and women's fashion"),
                    new Category("Books", "Programming and novels"),
                    new Category("Home & Kitchen", "Household products"),
                    new Category("Sports", "Sports equipment")
                };

                context.Categories.AddRange(categories);
                await context.SaveChangesAsync();

                // lookup ids after save
                var catMap = await context.Categories
                    .ToDictionaryAsync(c => c.Name, c => c.Id);

                var products = new List<Product>
                {
                    new Product("iPhone 16 Pro", "Latest iPhone 16 Pro", 1200m, null, 15, "IP16P", null, catMap["Electronics"]),
                    new Product("Samsung S26", "Samsung S26 flagship", 950m, null, 20, "SS26", null, catMap["Electronics"]),
                    new Product("MacBook Pro M5", "Apple MacBook Pro M5", 2500m, null, 8, "MBPM5", null, catMap["Electronics"]),
                    new Product("Dell XPS 15", "Dell XPS 15 laptop", 1800m, null, 12, "DX15", null, catMap["Electronics"]),
                    new Product("Logitech MX Master 3S", "Wireless mouse", 110m, null, 30, "LOG-MX3S", null, catMap["Electronics"]),
                    new Product("Mechanical Keyboard", "RGB mechanical keyboard", 80m, null, 25, "MECH-KB", null, catMap["Electronics"]),

                    new Product("Clean Architecture Book", "Book about Clean Architecture", 45m, null, 40, "BOOK-CA", null, catMap["Books"]),
                    new Product("ASP.NET Core in Action", "ASP.NET Core guide", 55m, null, 18, "BOOK-ASP", null, catMap["Books"]),

                    new Product("Nike Air Max", "Running shoes", 150m, null, 20, "NIKE-AM", null, catMap["Sports"]),
                    new Product("Adidas Running Shoes", "Comfortable running shoes", 130m, null, 18, "ADID-RS", null, catMap["Sports"]),

                    new Product("Hoodie", "Cozy hoodie", 40m, null, 35, "CLO-HOOD", null, catMap["Clothing"]),
                    new Product("T-Shirt", "Casual t-shirt", 20m, null, 50, "CLO-TSH", null, catMap["Clothing"]),
                    new Product("Jeans", "Blue jeans", 60m, null, 25, "CLO-JEANS", null, catMap["Clothing"]),

                    new Product("Coffee Maker", "Automatic coffee maker", 90m, null, 15, "HOME-CM", null, catMap["Home & Kitchen"]),
                    new Product("Blender", "Kitchen blender", 70m, null, 12, "HOME-BL", null, catMap["Home & Kitchen"])
                };

                context.Products.AddRange(products);
                await context.SaveChangesAsync();
            }

            // Create admin/customer users using UserManager if available
            // This block attempts to resolve UserManager<ApplicationUser> and RoleManager<...>.
            // If your app uses custom user class name or different namespaces, adjust types/names accordingly.
            try
            {
                var userManagerType = Type.GetType("Microsoft.AspNetCore.Identity.UserManager`1, Microsoft.AspNetCore.Identity");
                // resolve via DI: try to get a UserManager<object> instance (will be the real generic type)
                var userManager = provider.GetService(typeof(Microsoft.AspNetCore.Identity.UserManager<>).MakeGenericType(Type.GetType("ApplicationUser") ?? typeof(object)));
                if (userManager != null)
                {
                    // If you have an ApplicationUser type, use the following manual code block in Program.cs instead of this reflection-based attempt.
                    // See Program.cs snippet below for a straightforward example using ApplicationUser and UserManager.
                }
            }
            catch
            {
                // silent: if Identity services are named differently, create users manually using the pattern below.
            }
        }
    }
}