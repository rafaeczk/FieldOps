using FieldOps.Modules.Jobs.Application.Jobs.DTOs;
using FieldOps.Modules.Jobs.Application.Jobs.Repositories;
using FieldOps.Shared.Abstractions.Messages;

namespace FieldOps.Modules.Jobs.Application.Jobs.Queries;

public record GetJobQuery(Guid JobId) : IMessage<JobDto?>;

internal sealed class GetJobQueryHandler(IJobsReadRepository repository) : IMessageHandler<GetJobQuery, JobDto?>
{
    public Task<JobDto?> HandleAsync(GetJobQuery message, CancellationToken ct)
    {
        return repository.GetAsync(message.JobId);
    }
}
