using FieldOps.Shared.Abstractions.Events;
using FieldOps.Shared.Abstractions.Kernel.Ids;

namespace FieldOps.Modules.Reports.Contracts.Events;

public record ReportAssigneeRemoved(ReportId ReportId, FileId FileId) : IIntegrationEvent;
