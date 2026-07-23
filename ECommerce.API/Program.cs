using ECommerce.API.Extensions;
using ECommerce.Application;
using ECommerce.Infrastructure;
using ECommerce.Infrastructure.Persistence;
using Scalar.AspNetCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddApplication();

builder.Services.AddInfrastructure(
    builder.Configuration);

builder.Services.AddPresentation();
builder.Services.AddProblemDetails();
builder.Services.AddResponseCompression();
builder.Services.AddRateLimiter();

builder.Services.AddOpenApi();

builder.Services.AddHealthChecks()
    .AddDbContextCheck<ApplicationDbContext>();



var app = builder.Build();


app.ConfigurePipeline();

app.MapScalarApiReference();

app.MapHealthChecks("/health");
app.UseRateLimiter();
app.UseExceptionHandler();
app.UseResponseCompression();

using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    await DatabaseSeeder.SeedAsync(services);
}


app.Run();