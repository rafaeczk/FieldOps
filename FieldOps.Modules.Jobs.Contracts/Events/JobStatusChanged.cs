using FieldOps.Shared.Abstractions.Events;

namespace FieldOps.Modules.Jobs.Contracts.Events;

public record JobStatusChanged(Guid JobId, string Status) : IIntegrationEvent;
