using FieldOps.Modules.Jobs.Application.Common;
using FieldOps.Modules.Jobs.Application.Jobs.Exceptions;
using FieldOps.Modules.Jobs.Application.Jobs.Services;
using FieldOps.Modules.Jobs.Domain.Jobs.Repositories;
using FieldOps.Modules.Jobs.Domain.Outbox;
using FieldOps.Modules.Operators.Contracts;
using FieldOps.Shared.Abstractions.Contexts;
using FieldOps.Shared.Abstractions.Messages;

namespace FieldOps.Modules.Jobs.Application.Jobs.Commands;

public record DeleteJobCommand(Guid JobId) : IMessage;

internal sealed class DeleteJobCommandHandler(
    IJobsRepository repository,
    IOutboxMessagesRepository outboxRepository,
    IJobsUnitOfWork unitOfWork,
    IOperatorsModuleApi operatorsModuleApi,
    IJobEventMapper eventMapper,
    IContext context) : IMessageHandler<DeleteJobCommand>
{
    public async Task HandleAsync(DeleteJobCommand message, CancellationToken ct)
    {
        var job = await repository.GetAsync(message.JobId);

        if (job is null)
            throw new JobNotFoundException(message.JobId);

        if (context.Identity.Role != "ADMIN")
        {
            var operatorId = await operatorsModuleApi.GetOperatorIdByAccountId(context.Identity.Id);

            if (operatorId is null)
                throw new UnauthorizedAccessException();

            job.EnsureCanBeEdited(operatorId);
        }

        await repository.DeleteAsync(job);
        await outboxRepository.AddAsync([.. eventMapper.Map(job.Events)]);
        await unitOfWork.SaveChangesAsync(ct);
    }
}
