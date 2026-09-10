using FieldOps.Shared.Abstractions.Events;
using FieldOps.Shared.Abstractions.Kernel.Ids;
using System;
using System.Collections.Generic;
using System.Text;

namespace FieldOps.Modules.Reports.Contracts.Events
{
    public record ReportAssigneeRemoved(ReportId ReportId, FileId FileId) : IIntegrationEvent;
}
