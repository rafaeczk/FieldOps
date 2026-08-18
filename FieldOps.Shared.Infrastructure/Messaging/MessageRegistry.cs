using FieldOps.Shared.Abstractions.Events;
using Microsoft.Extensions.Options;

namespace FieldOps.Shared.Infrastructure.Messaging;

internal class MessageRegistry : IMessageRegistry
{
    private readonly IEventDispatcher eventDispatcher;
    private readonly List<MessageBroadcastRegistration> registrations = [];

    public MessageRegistry(IOptions<MessageRegistryOptions> options, IEventDispatcher eventDispatcher)
    {
        this.eventDispatcher = eventDispatcher;

        foreach (Type eventType in options.Value.BroadcastActionEventTypes)
        {
            AddBroadcastAction(eventType);
        }
    }

    public void AddBroadcastAction(Type eventType)
    {
        var eventDispatcherType = eventDispatcher.GetType();

        if (string.IsNullOrWhiteSpace(eventType.Namespace))
            throw new InvalidOperationException("Missing namespace.");

        var registration = new MessageBroadcastRegistration(
            eventType,
            @event =>
            {
                var x = eventDispatcherType.GetMethod(nameof(eventDispatcher.PublishAsync))?.MakeGenericMethod(eventType).Invoke(eventDispatcher, [@event]);
                return x is null ? throw new InvalidOperationException("'PublishAsync' method not found.") : (Task)x;
            });
        registrations.Add(registration);
    }

    public IEnumerable<MessageBroadcastRegistration> GetBroadcastRegistrations(string key)
        => registrations.Where(r => r.Key == key);
}
