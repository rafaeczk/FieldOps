using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using System.Runtime.CompilerServices;
using FieldOps.Modules.Files.Core;

[assembly: InternalsVisibleTo("FieldOps.Bootstrapper")]
namespace FieldOps.Modules.Files.Api;

internal static class Extensions
{
    public static IServiceCollection AddFilesModule(this IServiceCollection services)
    {
        services.AddCore();

        return services;
    }

    public static WebApplication UseFilesModule(this WebApplication app)
    {
        return app;
    }
}
