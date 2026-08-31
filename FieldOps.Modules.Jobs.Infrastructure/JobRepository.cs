using FieldOps.Modules.Jobs.Domain.Jobs.Entities;
using FieldOps.Modules.Jobs.Domain.Jobs.Repositories;
using FieldOps.Shared.Abstractions.Kernel.Types;
using System;
using System.Collections.Generic;
using System.Text;

namespace FieldOps.Modules.Jobs.Infrastructure
{
    internal class JobRepository : IJobRepository
    {
        public void Add(Job job)
        {
            throw new NotImplementedException();
        }

        public Task<Job> GetAsync(AggregateId id)
        {
            throw new NotImplementedException();
        }

        public void Update(Job job)
        {
            throw new NotImplementedException();
        }
    }
}
