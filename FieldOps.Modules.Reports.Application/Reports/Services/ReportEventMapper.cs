using FieldOps.Modules.Reports.Domain.Reports.Events;
using FieldOps.Shared.Abstractions.Events;
using FieldOps.Shared.Abstractions.Kernel;

namespace FieldOps.Modules.Reports.Application.Reports.Services;

internal class ReportEventMapper : IReportEventMapper
{
    public IIntegrationEvent? Map(IDomainEvent @event)
    => @event switch
    {
        ReportAdded e => new Contracts.Events.ReportAdded(e.Report.Id, e.Report.CreatorId),
        ReportAttachmentAdded e => new Contracts.Events.ReportAssigneeAdded(e.ReportAttachment.ReportId, e.ReportAttachment.FileId),
        ReportAttachmentRemoved e => new Contracts.Events.ReportAssigneeRemoved(e.ReportAttachment.ReportId, e.ReportAttachment.FileId),
        _ => null
    };

    public IEnumerable<IIntegrationEvent> Map(IEnumerable<IDomainEvent> events)
        => events.Select(Map).Where(e => e is not null);
}
