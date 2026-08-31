using FieldOps.Modules.Jobs.Domain.Jobs.Entities;
using FieldOps.Shared.Abstractions.Kernel.Types;

namespace FieldOps.Modules.Jobs.Domain.Jobs.Repositories;

public interface IJobsRepository
{
    Task<Job?> GetAsync(AggregateId id);
    void Add(Job job);
    void Update(Job job);
}
