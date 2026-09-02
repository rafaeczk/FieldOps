using FieldOps.Modules.Jobs.Application.Common;
using FieldOps.Modules.Jobs.Application.Jobs.Exceptions;
using FieldOps.Modules.Jobs.Application.Jobs.Services;
using FieldOps.Modules.Jobs.Domain.Jobs.Repositories;
using FieldOps.Modules.Jobs.Domain.Jobs.ValueObjects;
using FieldOps.Modules.Jobs.Domain.Outbox;
using FieldOps.Modules.Operators.Contracts;
using FieldOps.Shared.Abstractions.Contexts;
using FieldOps.Shared.Abstractions.Messages;

namespace FieldOps.Modules.Jobs.Application.Jobs.Commands;

public record EditJobCommand(Guid JobId, string Title, string? Description, JobPriority Priority, Address Address, DateTime Deadline) : IMessage;

internal sealed class EditJobCommandHandler(IJobsRepository repository, IOutboxMessagesRepository outboxRepository, IJobsUnitOfWork unitOfWork, 
    IOperatorsModuleApi operatorsModuleApi, IEventMapper eventMapper, IContext context) : IMessageHandler<EditJobCommand>
{
    public async Task HandleAsync(EditJobCommand message, CancellationToken ct)
    {
        var job = await repository.GetAsync(message.JobId);

        if (job is null)
            throw new JobNotFoundException(message.JobId);

        var operatorId = await operatorsModuleApi.GetOperatorIdByAccountId(context.Identity.Id);

        if (operatorId is null)
            throw new UnauthorizedAccessException();

        job.EnsureCanBeEdited(operatorId);

        job.ChangeTitle(message.Title);
        job.ChangeDescription(message.Description);
        job.ChangePriority(message.Priority);
        job.ChangeAddress(message.Address);
        job.ChangeDeadline(message.Deadline);

        await repository.UpdateAsync(job);
        await outboxRepository.AddAsync([.. eventMapper.Map(job.Events)]);
        await unitOfWork.SaveChangesAsync(ct);
    }
}
