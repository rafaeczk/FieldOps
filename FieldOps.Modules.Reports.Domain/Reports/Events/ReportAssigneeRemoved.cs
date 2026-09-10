using FieldOps.Modules.Reports.Domain.Reports.Entities;
using FieldOps.Shared.Abstractions.Kernel;
using System;
using System.Collections.Generic;
using System.Text;

namespace FieldOps.Modules.Reports.Domain.Reports.Events
{
    public record ReportAssigneeRemoved(ReportAttachment ReportAttachment) : IDomainEvent;
}
