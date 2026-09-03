using FieldOps.Shared.Abstractions.Events;
using FieldOps.Shared.Abstractions.Kernel.Ids;

namespace FieldOps.Modules.Jobs.Contracts.Events;

public record JobAssigneeRemoved(JobId JobId, TechnicianId TechnicianId) : IIntegrationEvent;
