using System;
using System.Collections.Generic;
using System.Text;

namespace FieldOps.Modules.Jobs.Contracts
{
    public interface IJobsModuleApi
    {
        Task<bool> Exists(Guid jobId, CancellationToken ct = default);
    }
}
