using FieldOps.Modules.Reports.Domain.Reports.Events;
using FieldOps.Shared.Abstractions.Events;
using FieldOps.Shared.Abstractions.Kernel;
using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Text;

namespace FieldOps.Modules.Reports.Application.Reports.Services
{
    internal class ReportEventMapper : IReportEventMapper
    {
        public IIntegrationEvent? Map(IDomainEvent @event)
        => @event switch
        {
            ReportAdded e => new Contracts.Events.ReportAdded(e.Report.Id, e.Report.CreatorId),
            ReportAssigneeAdded e => new Contracts.Events.ReportAssigneeAdded(e.ReportAttachment.ReportId, e.ReportAttachment.FileId),
            ReportAssigneeRemoved e => new Contracts.Events.ReportAssigneeRemoved(e.ReportAttachment.ReportId, e.ReportAttachment.FileId),
            _ => null
        };

        public IEnumerable<IIntegrationEvent> Map(IEnumerable<IDomainEvent> events)
            => events.Select(Map).Where(e => e is not null);
    }
}
