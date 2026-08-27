using FieldOps.Shared.Abstractions.Events;

namespace FieldOps.Modules.Reports.Core.Events;

internal record ReportSyncedEvent(
    Guid Id,
    Guid WorkOrderId,
    Guid TechnicianId) : IEvent;
