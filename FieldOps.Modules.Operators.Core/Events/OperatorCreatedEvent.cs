using FieldOps.Shared.Abstractions.Events;

namespace FieldOps.Modules.Operators.Core.Events;

internal record OperatorCreatedEvent(
    Guid Id,
    string FullName,
    DateTime CreatedAt,
    Guid RequestedAccountId,
    string RequestedEmail,
    string RequestedPassword) : IEvent;
