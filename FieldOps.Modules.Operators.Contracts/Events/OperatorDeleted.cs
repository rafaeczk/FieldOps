using MediatR;

namespace FieldOps.Modules.Operators.Contracts.Events;

public record OperatorDeleted(
    Guid AccountId
) : INotification;
