using FieldOps.Shared.Abstractions.Events;

namespace FieldOps.Modules.Accounts.Core.Events.Foreign.OperatorCreated;

internal record OperatorCreatedEvent(
    Guid Id,
    string FullName,
    DateTime CreatedAt,
    Guid RequestedAccountId,
    string RequestedEmail,
    string RequestedPassword) : IEvent;
