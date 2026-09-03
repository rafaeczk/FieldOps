using FieldOps.Shared.Abstractions.Kernel;
using Microsoft.Extensions.DependencyInjection;

namespace FieldOps.Shared.Infrastructure.Kernel;

internal sealed class DomainEventDispatcher(IServiceProvider serviceProvider) : IDomainEventDispatcher
{
    private readonly IServiceProvider serviceProvider = serviceProvider;

    public async Task DispatchAsync(params IDomainEvent[] _events)
    {
        var events = _events.Where(e => e is not null);

        if (!events.Any())
            return;

        using var scope = serviceProvider.CreateScope();

        foreach (var @event in events)
        {
            var handlerType = typeof(IDomainEventHandler<>).MakeGenericType(@event.GetType());
            IEnumerable<object?> handlers = scope.ServiceProvider.GetServices(handlerType) ?? [];

            var tasks = handlers.Select(h =>
            {
                if (h is null) return Task.CompletedTask;
                var method = handlerType.GetMethod(nameof(IDomainEventHandler<>.HandleAsync));
                if (method is null) return Task.CompletedTask;
                return (Task)method.Invoke(h, [@event]);
            });

            await Task.WhenAll(tasks);
        }
    }
}
