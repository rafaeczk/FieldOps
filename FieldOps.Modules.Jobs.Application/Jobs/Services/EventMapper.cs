using FieldOps.Modules.Jobs.Domain.Jobs.Events;
using FieldOps.Shared.Abstractions.Events;
using FieldOps.Shared.Abstractions.Kernel;

namespace FieldOps.Modules.Jobs.Application.Jobs.Services;

internal class EventMapper : IEventMapper
{
    public IIntegrationEvent? Map(IDomainEvent @event)
        => @event switch
        {
            JobAdded e => new Contracts.Events.JobAdded(e.Job.Id, e.Job.CreatorId),
            JobStatusChanged e => new Contracts.Events.JobStatusChanged(e.Job.Id, e.Status),
            JobAssigneeAdded e => new Contracts.Events.JobAssigneeAdded(e.JobAssignee.JobId, e.JobAssignee.TechnicianId),
            JobAssigneeRemoved e => new Contracts.Events.JobAssigneeRemoved(e.JobAssignee.JobId, e.JobAssignee.TechnicianId),
            _ => null
        };

    public IEnumerable<IIntegrationEvent> Map(IEnumerable<IDomainEvent> events)
        => events.Select(Map).Where(e => e is not null);
}
