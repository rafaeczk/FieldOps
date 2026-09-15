using FieldOps.Shared.Abstractions.Events;
using FieldOps.Shared.Abstractions.Kernel.Ids;

namespace FieldOps.Modules.Reports.Contracts.Events;

public record ReportAttachmentRemoved(ReportId ReportId, FileId FileId) : IIntegrationEvent;
