using FieldOps.Shared.Abstractions.Kernel.Ids;

namespace FieldOps.Modules.Reports.Application.Reports.Specifications;

public class GetReportSpecification : ReportsBaseSpecification
{
    public GetReportSpecification(ReportId reportId, string role, TechnicianId? technicianId) : base(role, technicianId)
    {
        AddCriteria(r => r.Id == reportId);

        AddInclude(r => r.Attachments);
        AddInclude(r => r.ReportAssets);
    }
}
