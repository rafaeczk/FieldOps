using MediatR;

namespace FieldOps.Modules.Operators.Contracts.Events;

public record OperatorCreated(
    Guid Id,
    string FullName,
    DateTime CreatedAt,
    Guid RequestedAccountId,
    string RequestedEmail,
    string RequestedPassword) : INotification;
