using FieldOps.Shared.Infrastructure.Modules;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System.Text;

namespace FieldOps.Shared.Infrastructure.Events;

public class OutboxProcessorWorker<OutboxRepo>(
    IServiceScopeFactory scopeFactory, 
    string moduleName, 
    Dictionary<string, Type> typeMapping,
    ILogger<OutboxProcessorWorker<OutboxRepo>> logger) : BackgroundService
    where OutboxRepo : IModuleOutboxRepository
{
    private readonly IServiceScopeFactory scopeFactory = scopeFactory;
    private readonly string moduleName = moduleName;
    private readonly Dictionary<string, Type> typeMapping = typeMapping;
    private readonly ILogger<OutboxProcessorWorker<OutboxRepo>> logger = logger;

    protected override async Task ExecuteAsync(CancellationToken ct)
    {
        logger.LogInformation("Outbox processor for module {ModuleName} started.", moduleName);

        while (!ct.IsCancellationRequested)
        {
            try
            {
                using var scope = scopeFactory.CreateScope();

                var outboxRepo = scope.ServiceProvider.GetRequiredService<OutboxRepo>();
                var publisher = scope.ServiceProvider.GetRequiredService<IPublisher>();
                var serializer = scope.ServiceProvider.GetRequiredService<IModuleSerializer>();

                var messages = await outboxRepo.BrowseUnprocessedAsync(20);

                foreach (var message in messages)
                {
                    var eventType = typeMapping.GetValueOrDefault(message.Type);
                    if (eventType is null) continue;

                    var @event = serializer.Deserialize(
                        Encoding.UTF8.GetBytes(message.Content), 
                        eventType);

                    await publisher.Publish(@event, ct);
                    await outboxRepo.MarkAsProcessedAsync(message.Id);
                    logger.LogDebug("Processed outbox message {MessageId} of type {EventType} in module {ModuleName}.", message.Id, message.Type, moduleName);
                }
            }
            catch(Exception ex)
            {
                logger.LogError(ex, "An error occurred while processing outbox messages in module {ModuleName}.", moduleName);
            }

            await Task.Delay(TimeSpan.FromSeconds(3), ct);
        }
    }
}
