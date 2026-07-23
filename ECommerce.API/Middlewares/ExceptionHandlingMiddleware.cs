using Microsoft.AspNetCore.Mvc;

namespace ECommerce.API.Middlewares;

public sealed class ExceptionHandlingMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<ExceptionHandlingMiddleware> _logger;

    public ExceptionHandlingMiddleware(
        RequestDelegate next,
        ILogger<ExceptionHandlingMiddleware> logger)
    {
        _next = next;
        _logger = logger;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex,
                "Unhandled exception occurred.");

            context.Response.StatusCode =
                StatusCodes.Status500InternalServerError;

            var problem = new ProblemDetails
            {
                Status = 500,
                Title = "Internal Server Error",
                Detail = "An unexpected error occurred."
            };

            await context.Response.WriteAsJsonAsync(problem);
        }
    }
}