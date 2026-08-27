using FieldOps.Shared.Abstractions.Events;

namespace FieldOps.Modules.Reports.Core.Events;

internal record ReportCreatedEvent(
    Guid Id,
    Guid WorkOrderId,
    Guid TechnicianId) : IEvent;
