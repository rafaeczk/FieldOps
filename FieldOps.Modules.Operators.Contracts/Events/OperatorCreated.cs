using FieldOps.Shared.Abstractions.Events;

namespace FieldOps.Modules.Operators.Contracts.Events;

public record OperatorCreated(
    Guid Id,
    string FullName,
    DateTime CreatedAt,
    Guid RequestedAccountId,
    string RequestedEmail,
    string RequestedPassword) : IIntegrationEvent;
