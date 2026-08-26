using FieldOps.Modules.WorkOrders.Core;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using System.Runtime.CompilerServices;

[assembly: InternalsVisibleTo("FieldOps.Bootstrapper")]
namespace FieldOps.Modules.WorkOrders.Api;

internal static class Extensions
{
    public static IServiceCollection AddWorkOrdersModule(this IServiceCollection services)
    {
        services.AddCore();

        return services;
    }

    public static WebApplication UseWorkOrdersModule(this WebApplication app)
    {
        return app;
    }
}
