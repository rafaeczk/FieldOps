using FieldOps.Modules.Jobs.Application.Jobs.Repositories;
using FieldOps.Shared.Abstractions.Messages;
using System;
using System.Collections.Generic;
using System.Text;

namespace FieldOps.Modules.Jobs.Application.Jobs.Queries
{
    public record CheckJobId(Guid JobId) : IMessage<bool>;

    internal class CheckJobIdHandler(IJobsReadRepository repository) : IMessageHandler<CheckJobId, bool>
    {
        public async Task<bool> HandleAsync(CheckJobId message, CancellationToken ct)
        {
            if (message.JobId == Guid.Empty)
            {
                return false;
            }

            return await repository.ExistsAsync(message.JobId, ct);
        }
    }
}
