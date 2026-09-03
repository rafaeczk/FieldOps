using FieldOps.Shared.Abstractions.Kernel;

namespace FieldOps.Shared.Abstractions.Events;

public interface IEventMapper
{
    IIntegrationEvent? Map(IDomainEvent @event);
    IEnumerable<IIntegrationEvent> Map(IEnumerable<IDomainEvent> events);
}
