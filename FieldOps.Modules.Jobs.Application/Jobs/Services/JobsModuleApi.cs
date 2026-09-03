using FieldOps.Modules.Jobs.Contracts;
using Microsoft.AspNetCore.Hosting.Server;
using System;
using System.Collections.Generic;
using System.Text;
using MediatR;
using FieldOps.Modules.Jobs.Application.Jobs.Queries;

namespace FieldOps.Modules.Jobs.Application.Jobs.Services
{
    internal class JobsModuleApi(ISender sender) : IJobsModuleApi
    {
        private readonly ISender sender = sender;

        public Task<bool> Exists(Guid jobId, CancellationToken ct = default)
        {
            return sender.Send(new CheckJobId(jobId), ct);
        }
    }
}
