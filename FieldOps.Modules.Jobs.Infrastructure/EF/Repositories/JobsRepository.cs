using FieldOps.Modules.Jobs.Domain.Jobs.Entities;
using FieldOps.Modules.Jobs.Domain.Jobs.Repositories;
using FieldOps.Shared.Abstractions.Kernel;
using FieldOps.Shared.Abstractions.Kernel.Ids;
using Microsoft.EntityFrameworkCore;

namespace FieldOps.Modules.Jobs.Infrastructure.EF.Repositories;

internal sealed class JobsRepository(JobsDbContext context, IDomainEventDispatcher domainEventDispatcher) : IJobsRepository
{
    private readonly JobsDbContext context = context;
    private readonly IDomainEventDispatcher domainEventDispatcher = domainEventDispatcher;

    public async Task AddAsync(Job job)
    {
        context.Jobs.Add(job);
        await domainEventDispatcher.DispatchAsync([.. job.Events]);
    }

    public Task<Job?> GetAsync(JobId id)
    {
        return context.Jobs.Include(j => j.Assignees).SingleOrDefaultAsync(j => j.Id.Equals(id));
    }

    public async Task UpdateAsync(Job job, int version)
    {
        context.Jobs.Update(job);
        context.Entry(job).Property(x => x.Version).OriginalValue = version;
        await domainEventDispatcher.DispatchAsync([.. job.Events]);
    }

    public async Task DeleteAsync(Job job)
    {
        context.Jobs.Remove(job);
        await domainEventDispatcher.DispatchAsync([.. job.Events]);
    }
}
