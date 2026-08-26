using FieldOps.Shared.Abstractions.Events;

namespace FieldOps.Modules.WorkOrders.Core.Events;

internal record WorkOrderStatusChangedEvent(
    Guid Id,
    string Status,
    Guid? TechnicianId,
    Guid OperatorId) : IEvent;
