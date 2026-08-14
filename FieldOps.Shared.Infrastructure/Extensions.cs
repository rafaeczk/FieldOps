using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using FieldOps.Shared.Abstractions.Time;
using FieldOps.Shared.Infrastructure.Api;
using FieldOps.Shared.Infrastructure.Errors;
using FieldOps.Shared.Infrastructure.Services;
using FieldOps.Shared.Infrastructure.Time;
using System.Runtime.CompilerServices;

[assembly: InternalsVisibleTo("FieldOps.Bootstrapper")]
namespace FieldOps.Shared.Infrastructure;

internal static class Extensions
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services)
    {
        services.AddErrorHandling();
        services.AddSingleton<IClock, Clock>();
        services.AddHostedService<AppInitializer>();

        services.AddControllers()
            .ConfigureApplicationPartManager(manager =>
            {
                manager.FeatureProviders.Add(new InternalControllerFeatureProvider());
            });

        return services;
    }

    public static WebApplication UseInfrastructure(this WebApplication app)
    {
        app.UseErrorHandling();

        app.UseHttpsRedirection();

        app.UseAuthentication();

        app.MapControllers();
        app.MapGet("/", static context => context.Response.WriteAsync("FieldOpsApi"));

        app.UseAuthorization();

        return app;
    }

    public static T GetOptions<T>(this IServiceCollection services, string sectionName)
        where T : new()
    {
        using var provider = services.BuildServiceProvider();
        var configuration = provider.GetRequiredService<IConfiguration>();

        var options = new T();
        configuration.GetSection(sectionName).Bind(options);

        return options;
    }
}
