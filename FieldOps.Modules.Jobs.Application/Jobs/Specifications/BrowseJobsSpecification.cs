using FieldOps.Shared.Abstractions.Kernel.Ids;
using FieldOps.Shared.Abstractions.Pagination;

namespace FieldOps.Modules.Jobs.Application.Jobs.Specifications;

public class BrowseJobsSpecification : JobsBaseSpecification
{
    public BrowseJobsSpecification(string role, TechnicianId? technicianId, PaginationParams pagination, Guid? assigneeId = null) : base(role, technicianId)
    {
        if (assigneeId.HasValue)
        {
            AddCriteria(j => j.Assignees.Any(a => a.TechnicianId.Equals(new TechnicianId(assigneeId.Value))));
        }

        ApplyOrderByDescending(j => j.CreatedAt);

        ApplyPaging(pagination);
    }
}
