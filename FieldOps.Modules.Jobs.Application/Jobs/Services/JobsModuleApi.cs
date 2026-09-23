using FieldOps.Modules.Jobs.Contracts;
using FieldOps.Modules.Jobs.Application.Jobs.Queries;
using FieldOps.Shared.Abstractions.Messages;

namespace FieldOps.Modules.Jobs.Application.Jobs.Services
{
    internal class JobsModuleApi(IMessageDispatcher messageDispatcher) : IJobsModuleApi
    {
        private readonly IMessageDispatcher messageDispatcher = messageDispatcher;

        public Task<bool> Exists(Guid jobId, CancellationToken ct = default)
        {
            return messageDispatcher.Send(new CheckJobId(jobId), ct);
        }
    }
}
