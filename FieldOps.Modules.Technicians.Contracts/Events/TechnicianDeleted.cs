using FieldOps.Shared.Abstractions.Events;

namespace FieldOps.Modules.Technicians.Contracts.Events;

public record TechnicianDeleted(
    Guid AccountId
) : IIntegrationEvent;
