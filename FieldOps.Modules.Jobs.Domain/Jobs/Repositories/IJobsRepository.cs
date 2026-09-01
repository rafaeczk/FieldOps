using FieldOps.Modules.Jobs.Domain.Jobs.Entities;
using FieldOps.Shared.Abstractions.Kernel.Types;

namespace FieldOps.Modules.Jobs.Domain.Jobs.Repositories;

public interface IJobsRepository
{
    Task<Job?> GetAsync(AggregateId id);
    Task AddAsync(Job job);
    Task UpdateAsync(Job job);
}
