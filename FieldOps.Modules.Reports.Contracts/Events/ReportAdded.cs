using FieldOps.Shared.Abstractions.Events;
using System;
using System.Collections.Generic;
using System.Text;

namespace FieldOps.Modules.Reports.Contracts.Events
{
    public record ReportAdded(Guid Id, Guid CreatorId) : IIntegrationEvent;
}
