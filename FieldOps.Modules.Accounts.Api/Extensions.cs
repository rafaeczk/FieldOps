using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using System.Runtime.CompilerServices;
using FieldOps.Modules.Accounts.Core;

[assembly: InternalsVisibleTo("FieldOps.Bootstrapper")]
namespace FieldOps.Modules.Accounts.Api;

internal static class Extensions
{
    public static IServiceCollection AddAccountsModule(this IServiceCollection services)
    {
        services.AddCore();

        return services;
    }

    public static WebApplication UseAccountsModule(this WebApplication app)
    {
        return app;
    }
}
