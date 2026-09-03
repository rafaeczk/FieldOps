using FieldOps.Shared.Abstractions.Events;

namespace FieldOps.Modules.Operators.Contracts.Events;

public record OperatorDeleted(
    Guid AccountId
) : IIntegrationEvent;
