using FieldOps.Shared.Abstractions.Events;
using FieldOps.Shared.Abstractions.Kernel.Ids;

namespace FieldOps.Modules.Reports.Contracts.Events;

public record ReportAssigneeAdded(ReportId ReportId, FileId FileId) : IIntegrationEvent;
