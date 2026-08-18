using FieldOps.Shared.Abstractions.Events;
using Microsoft.Extensions.DependencyInjection;

namespace FieldOps.Shared.Infrastructure.Events;

internal class EventDispatcher(IServiceProvider serviceProvider) : IEventDispatcher
{
    private readonly IServiceProvider serviceProvider = serviceProvider;

    public async Task PublishAsync<Event>(Event @event)
        where Event : class, IEvent
    {
        using var scope = serviceProvider.CreateScope();

        var handlers = scope.ServiceProvider.GetServices<IEventHandler<Event>>();

        var tasks = handlers.Select(h => h.HandleAsync(@event));

        await Task.WhenAll(tasks);
    }
}
