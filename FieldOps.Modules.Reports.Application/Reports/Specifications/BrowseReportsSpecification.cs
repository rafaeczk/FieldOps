using FieldOps.Modules.Reports.Application.Reports.Exceptions;
using FieldOps.Shared.Abstractions.Kernel.Ids;
using FieldOps.Shared.Abstractions.Kernel.ValueObjects;
using FieldOps.Shared.Abstractions.Pagination;

namespace FieldOps.Modules.Reports.Application.Reports.Specifications;

public class BrowseReportsSpecification : ReportsBaseSpecification
{
    public BrowseReportsSpecification(string role, TechnicianId? technicianId, JobId? jobId, PaginationParams pagination) : base(role, technicianId)
    {
        switch (role)
        {
            case AccountRole.Admin:
            case AccountRole.Operator:
                break;
            case AccountRole.Technician:
                AddCriteria(r => r.CreatorId.Equals(technicianId));
                break;
            default:
                throw new UnauthorizedReportAccessException();
        }

        if (jobId is not null)
            AddCriteria(r => r.JobId == jobId);

        AddCriteria(r => !r.IsDeleted);

        ApplyPaging(pagination);

        ApplyOrderByDescending(r => r.CreatedAt);
    }
}
