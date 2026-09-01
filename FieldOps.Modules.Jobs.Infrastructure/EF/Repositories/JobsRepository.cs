using FieldOps.Modules.Jobs.Domain.Jobs.Entities;
using FieldOps.Modules.Jobs.Domain.Jobs.Repositories;
using FieldOps.Shared.Abstractions.Kernel;
using FieldOps.Shared.Abstractions.Kernel.Types;
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

    public Task<Job?> GetAsync(AggregateId id)
    {
        return context.Jobs.SingleOrDefaultAsync(j => j.Id == id);
    }

    public async Task UpdateAsync(Job job)
    {
        context.Jobs.Update(job);
        await domainEventDispatcher.DispatchAsync([.. job.Events]);
    }
}
