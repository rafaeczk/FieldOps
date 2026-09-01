using FieldOps.Shared.Abstractions.Events;
using FieldOps.Shared.Abstractions.Kernel;

namespace FieldOps.Modules.Jobs.Application.Jobs.Services;

internal class EventMapper : IEventMapper
{
    public IIntegrationEvent? Map(IDomainEvent @event)
        => @event switch
        {
            Domain.Jobs.Events.JobAdded e => new Contracts.Events.JobAdded(e.Job.Id, e.Job.CreatorId),
            Domain.Jobs.Events.JobStatusChanged e => new Contracts.Events.JobStatusChanged(e.Job.Id, e.Status),
            _ => null
        };

    public IEnumerable<IIntegrationEvent> Map(IEnumerable<IDomainEvent> events)
        => events.Select(Map).Where(e => e is not null);
}
