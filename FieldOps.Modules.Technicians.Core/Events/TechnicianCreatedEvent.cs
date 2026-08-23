using FieldOps.Shared.Abstractions.Events;

namespace FieldOps.Modules.Technicians.Core.Events;

internal record TechnicianCreatedEvent(
    Guid Id,
    string FullName,
    DateTime CreatedAt,
    Guid RequestedAccountId,
    string RequestedEmail,
    string RequestedPassword) : IEvent;
