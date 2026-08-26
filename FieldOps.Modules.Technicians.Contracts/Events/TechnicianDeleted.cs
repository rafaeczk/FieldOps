using MediatR;

namespace FieldOps.Modules.Technicians.Contracts.Events;

public record TechnicianDeleted(
    Guid AccountId
) : INotification;
