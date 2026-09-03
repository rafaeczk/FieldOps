using FieldOps.Shared.Abstractions.Events;

namespace FieldOps.Modules.Jobs.Contracts.Events;

public record JobAdded(Guid Id, Guid CreatorId) : IIntegrationEvent;
