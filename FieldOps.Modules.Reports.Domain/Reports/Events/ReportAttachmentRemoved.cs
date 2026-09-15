using FieldOps.Modules.Reports.Domain.Reports.Entities;
using FieldOps.Shared.Abstractions.Kernel;

namespace FieldOps.Modules.Reports.Domain.Reports.Events;

public record ReportAttachmentRemoved(ReportAttachment ReportAttachment) : IDomainEvent;
