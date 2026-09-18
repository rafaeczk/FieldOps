using System.Reflection;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace FieldOps.Shared.Infrastructure.Services;

internal class AppInitializer(IServiceProvider serviceProvider, ILogger<AppInitializer> logger) : IHostedService
{
    private readonly IServiceProvider serviceProvider = serviceProvider;
    private readonly ILogger<AppInitializer> logger = logger;

    public async Task StartAsync(CancellationToken ct)
    {
        var dbContextTypes = AppDomain.CurrentDomain.GetAssemblies()
            .SelectMany(a =>
            {
                try { return a.GetTypes(); }
                catch (ReflectionTypeLoadException e) { return e.Types!; }
            })
            .Where(t => t is not null && typeof(DbContext).IsAssignableFrom(t) && typeof(DbContext) != t && !t.IsInterface);

        using var scope = serviceProvider.CreateScope();

        foreach (var dbContextType in dbContextTypes)
        {
            logger.LogInformation("Migrating {DbContext}", dbContextType.Name);

            if (scope.ServiceProvider.GetService(dbContextType) is not DbContext dbContext)
            {
                logger.LogWarning("DbContext {DbContext} not found in DI container", dbContextType.Name);
                continue;
            }

            try
            {
                await dbContext.Database.MigrateAsync(ct);
                logger.LogInformation("Successfully migrated {DbContext}", dbContextType.Name);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Failed to migrate {DbContext}", dbContextType.Name);
            }
        }
    }

    public Task StopAsync(CancellationToken ct) => Task.CompletedTask;
}
