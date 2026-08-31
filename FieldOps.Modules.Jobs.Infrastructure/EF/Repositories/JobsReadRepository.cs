using FieldOps.Modules.Jobs.Application.Jobs.DTOs;
using FieldOps.Modules.Jobs.Application.Jobs.Repositories;
using FieldOps.Shared.Abstractions.Pagination;
using FieldOps.Shared.Infrastructure.Pagination;
using Microsoft.EntityFrameworkCore;

namespace FieldOps.Modules.Jobs.Infrastructure.EF.Repositories;

internal class JobsReadRepository(JobsDbContext context) : IJobsReadRepository
{
    private readonly JobsDbContext context = context;

    public async Task<PagedResult<JobListItemDto>> BrowseAsync(PaginationParams pagination)
    {
        var totalItems = await context.Jobs.CountAsync();

        var jobs = await context.Jobs
            .OrderByDescending(j => j.CreatedAt)
            .Paginate(pagination)
            .Select(j => new JobListItemDto(j.Id, j.Title, j.Status.Value, j.Priority.Value, j.Deadline))
            .ToListAsync();

        return new(jobs, totalItems, pagination);
    }

    public async Task<JobDto?> GetAsync(Guid jobId)
    {
        var job = await context.Jobs.SingleOrDefaultAsync(j => j.Id == jobId);

        if (job is null) return null;

        return new(job.Id, job.Title, job.Description, job.Status, job.Priority, job.Address, job.Deadline, job.CreatedAt, job.UpdatedAt);
    }
}
