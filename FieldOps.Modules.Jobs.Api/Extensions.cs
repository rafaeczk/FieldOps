using FieldOps.Modules.Jobs.Application;
using FieldOps.Modules.Jobs.Domain;
using FieldOps.Modules.Jobs.Infrastructure;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using System.Runtime.CompilerServices;

[assembly: InternalsVisibleTo("FieldOps.Bootstrapper")]
namespace FieldOps.Modules.Jobs.Api;

internal static class Extensions
{
    public static IServiceCollection AddJobsModule(this IServiceCollection services)
    {
        return services
            .AddDomain()
            .AddApplication()
            .AddInfrastructure();
    }

    public static WebApplication UseJobsModule(this WebApplication app)
    {
        return app;
    }
}
