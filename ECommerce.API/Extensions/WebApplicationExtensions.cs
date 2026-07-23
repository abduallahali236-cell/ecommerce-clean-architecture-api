using ECommerce.API.Middlewares;

namespace ECommerce.API.Extensions;

public static class WebApplicationExtensions
{
    public static WebApplication ConfigurePipeline(
        this WebApplication app)
    {
        app.UseMiddleware<ExceptionHandlingMiddleware>();

        if (app.Environment.IsDevelopment())
        {
            app.MapOpenApi();
        }

        app.UseHttpsRedirection();

        app.UseAuthentication();

        app.UseAuthorization();

        app.MapControllers();

        return app;
    }
}