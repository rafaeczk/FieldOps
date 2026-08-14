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
            .SelectMany(a => a.GetTypes())
            .Where(t => typeof(DbContext).IsAssignableFrom(t) && typeof(DbContext) != t && t.IsInterface == false);

        using var scope = serviceProvider.CreateScope();

        foreach (var dbContextType in dbContextTypes)
        {
            if (scope.ServiceProvider.GetService(dbContextType) is not DbContext dbContext) continue;

            await dbContext.Database.MigrateAsync(ct);
        }
    }

    public Task StopAsync(CancellationToken cancellationToken) => Task.CompletedTask;
}
