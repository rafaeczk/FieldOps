using FieldOps.Modules.Jobs.Application.Common;
using FieldOps.Modules.Jobs.Application.Jobs.Exceptions;
using FieldOps.Modules.Jobs.Application.Jobs.Services;
using FieldOps.Modules.Jobs.Domain.Jobs.Repositories;
using FieldOps.Modules.Jobs.Domain.Outbox;
using FieldOps.Shared.Abstractions.Kernel.Ids;
using FieldOps.Shared.Abstractions.Messages;

namespace FieldOps.Modules.Jobs.Application.Jobs.Commands;

public record RemoveJobAssigneeCommand(JobId JobId, TechnicianId TechnicianId) : IMessage;

internal sealed class RemoveJobAssigneeCommandHandler(IJobsRepository repository, IOutboxMessagesRepository outboxRepository, IJobsUnitOfWork unitOfWork,
    IJobEventMapper eventMapper) : IMessageHandler<RemoveJobAssigneeCommand>
{
    public async Task HandleAsync(RemoveJobAssigneeCommand message, CancellationToken ct)
    {
        var job = await repository.GetAsync(message.JobId);

        if (job is null)
            throw new JobNotFoundException(message.JobId);

        job.RemoveAssignee(message.TechnicianId);
        await outboxRepository.AddAsync([.. eventMapper.Map(job.Events)]);
        await unitOfWork.SaveChangesAsync(ct);
    }
}
