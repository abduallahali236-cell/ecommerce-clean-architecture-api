namespace ECommerce.API.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddPresentation(
        this IServiceCollection services)
    {
        services.AddControllers();

        services.AddEndpointsApiExplorer();

        services.AddOpenApi();

        return services;
    }
}