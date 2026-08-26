using FieldOps.Shared.Abstractions.Events;

namespace FieldOps.Modules.WorkOrders.Core.Events;

internal record WorkOrderCreatedEvent(
    Guid Id,
    string Title,
    string Address,
    DateTime Deadline,
    Guid OperatorId) : IEvent;
