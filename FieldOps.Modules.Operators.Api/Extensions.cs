using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using System.Runtime.CompilerServices;
using FieldOps.Modules.Operators.Core;

[assembly: InternalsVisibleTo("FieldOps.Bootstrapper")]
namespace FieldOps.Modules.Operators.Api;

internal static class Extensions
{
    public static IServiceCollection AddOperatorsModule(this IServiceCollection services)
    {
        services.AddCore();

        return services;
    }

    public static WebApplication UseOperatorsModule(this WebApplication app)
    {
        return app;
    }
}
