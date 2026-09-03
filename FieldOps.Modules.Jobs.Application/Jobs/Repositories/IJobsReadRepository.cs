
using FieldOps.Modules.Jobs.Application.Jobs.DTOs;
using FieldOps.Shared.Abstractions.Pagination;

namespace FieldOps.Modules.Jobs.Application.Jobs.Repositories;

public interface IJobsReadRepository
{
    Task<PagedResult<JobListItemDto>> BrowseAsync(PaginationParams pagination);
    Task<JobDto?> GetAsync(Guid jobId);
}
