using FieldOps.Shared.Abstractions.Kernel.Ids;
using FieldOps.Shared.Abstractions.Pagination;

namespace FieldOps.Modules.Jobs.Application.Jobs.Specifications;

public class BrowseJobsSpecification : JobsBaseSpecification
{
    public BrowseJobsSpecification(string role, TechnicianId? technicianId, PaginationParams pagination) : base(role, technicianId)
    {
        ApplyOrderByDescending(j => j.CreatedAt);

        ApplyPaging(pagination);
    }
}
