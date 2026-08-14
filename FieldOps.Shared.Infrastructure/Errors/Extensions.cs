using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using FieldOps.Shared.Abstractions.Errors;

namespace FieldOps.Shared.Infrastructure.Errors;

internal static class Extensions
{
    public static IServiceCollection AddErrorHandling(this IServiceCollection services)
    {
        services.AddScoped<ErrorHandlingMiddleware>();
        services.AddSingleton<IExceptionToResponseMapper, ExceptionToResponseMapper>();
        services.AddSingleton<IExceptionCompositionRoot, ExceptionCompositionRoot>();

        return services;
    }

    public static WebApplication UseErrorHandling(this WebApplication app)
    {
        app.UseMiddleware<ErrorHandlingMiddleware>();

        return app;
    }
}
