using FieldOps.Modules.Reports.Core;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using System.Runtime.CompilerServices;

[assembly: InternalsVisibleTo("FieldOps.Bootstrapper")]
namespace FieldOps.Modules.Reports.Api;

internal static class Extensions
{
    public static IServiceCollection AddReportsModule(this IServiceCollection services)
    {
        services.AddCore();

        return services;
    }

    public static WebApplication UseReportsModule(this WebApplication app)
    {
        return app;
    }
}
