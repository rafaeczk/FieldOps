using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using System.Runtime.CompilerServices;

[assembly: InternalsVisibleTo("FieldOps.Bootstrapper")]
namespace FieldOps.Modules.Users.Api;

internal static class Extensions
{
    public static IServiceCollection AddUsersModule(this IServiceCollection services)
    {
        return services;
    }

    public static WebApplication UseUsersModule(this WebApplication app)
    {
        return app;
    }
}
