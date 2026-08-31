using FieldOps.Modules.Jobs.Domain.Jobs.Entities;
using FieldOps.Modules.Jobs.Domain.Jobs.Repositories;
using FieldOps.Shared.Abstractions.Kernel.Types;
using Microsoft.EntityFrameworkCore;

namespace FieldOps.Modules.Jobs.Infrastructure.EF.Repositories;

internal sealed class JobsRepository(JobsDbContext context) : IJobsRepository
{
    private readonly JobsDbContext context = context;

    public void Add(Job job)
    {
        context.Jobs.Add(job);
    }

    public Task<Job?> GetAsync(AggregateId id)
    {
        return context.Jobs.SingleOrDefaultAsync(j => j.Id == id);
    }

    public void Update(Job job)
    {
        context.Jobs.Update(job);
    }
}
