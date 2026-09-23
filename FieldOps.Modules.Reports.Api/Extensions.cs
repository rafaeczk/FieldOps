using FieldOps.Modules.Reports.Application;
using FieldOps.Modules.Reports.Domain;
using FieldOps.Modules.Reports.Infrastructure;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using System.Runtime.CompilerServices;

[assembly: InternalsVisibleTo("FieldOps.Bootstrapper")]
namespace FieldOps.Modules.Reports.Api;

internal static class Extensions
{
    public static IServiceCollection AddReportsModule(this IServiceCollection services)
    {
        return services
            .AddApplication()
            .AddInfrastructure();
    }

    public static WebApplication UseReportsModule(this WebApplication app)
    {
        return app;
    }
}
