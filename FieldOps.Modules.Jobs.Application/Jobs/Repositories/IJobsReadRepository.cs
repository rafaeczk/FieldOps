
using FieldOps.Modules.Jobs.Application.Jobs.DTOs;
using FieldOps.Modules.Jobs.Application.Jobs.Specifications;
using FieldOps.Shared.Abstractions.Pagination;

namespace FieldOps.Modules.Jobs.Application.Jobs.Repositories;

public interface IJobsReadRepository
{
    Task<PagedResult<JobListItemDto>> BrowseAsync(BrowseJobsSpecification spec);
    Task<JobDto?> GetAsync(Guid jobId);
    Task<bool> ExistsAsync(Guid id, CancellationToken ct = default);
}
