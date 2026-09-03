using FieldOps.Shared.Abstractions.Events;

namespace FieldOps.Modules.Technicians.Contracts.Events;

public record TechnicianCreated(
    Guid Id,
    string FullName,
    DateTime CreatedAt,
    Guid RequestedAccountId,
    string RequestedEmail,
    string RequestedPassword) : IIntegrationEvent;
