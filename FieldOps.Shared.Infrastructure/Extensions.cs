using FieldOps.Shared.Abstractions.Contexts;
using FieldOps.Shared.Abstractions.Time;
using FieldOps.Shared.Infrastructure.Api;
using FieldOps.Shared.Infrastructure.Auth;
using FieldOps.Shared.Infrastructure.Contexts;
using FieldOps.Shared.Infrastructure.Errors;
using FieldOps.Shared.Infrastructure.Messages;
using FieldOps.Shared.Infrastructure.Modules;
using FieldOps.Shared.Infrastructure.S3;
using FieldOps.Shared.Infrastructure.Services;
using FieldOps.Shared.Infrastructure.Time;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
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

        services.AddSingleton<IModuleSerializer, JsonModuleSerializer>();

        services.AddScoped<IContext>(sp =>
        {
            var httpContext = sp.GetRequiredService<IHttpContextAccessor>().HttpContext;
            return httpContext switch
            {
                null => Context.Empty,
                not null => new Context(httpContext)
            };
        });

        services.AddTransient(typeof(IRequestHandler<,>), typeof(MediatRMessageBridge<,>));
        services.AddTransient(typeof(IRequestHandler<>), typeof(MediatRVoidMessageBridge<>));

        services.AddS3();

        services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

        services.AddCors();

        services.AddControllers()
            .ConfigureApplicationPartManager(manager =>
            {
                manager.FeatureProviders.Add(new InternalControllerFeatureProvider());
            });

        services.AddAuth();

        services.AddSwaggerGen(swagger =>
        {
            swagger.CustomSchemaIds(x => x.FullName);
        });

        return services;
    }

    public static WebApplication UseInfrastructure(this WebApplication app)
    {
        app.UseErrorHandling();
        app.UseHttpsRedirection();

        app.UseCors();

        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.UseRouting();

        app.UseAuthentication();
        app.UseAuthorization();

        app.MapControllers();
        app.MapGet("/", static context => context.Response.WriteAsync("FieldOpsApi"));

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
