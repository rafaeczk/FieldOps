using FieldOps.Modules.Jobs.Domain.Jobs.Entities;
using FieldOps.Shared.Abstractions.Kernel.Ids;

namespace FieldOps.Modules.Jobs.Domain.Jobs.Repositories;

public interface IJobsRepository
{
    Task<Job?> GetAsync(JobId id);
    Task AddAsync(Job job);
    Task UpdateAsync(Job job);
}
