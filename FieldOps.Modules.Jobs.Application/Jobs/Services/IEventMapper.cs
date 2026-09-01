using FieldOps.Shared.Abstractions.Events;
using FieldOps.Shared.Abstractions.Kernel;

namespace FieldOps.Modules.Jobs.Application.Jobs.Services;

public interface IEventMapper
{
    IIntegrationEvent? Map(IDomainEvent @event);
    IEnumerable<IIntegrationEvent> Map(IEnumerable<IDomainEvent> events);
}
