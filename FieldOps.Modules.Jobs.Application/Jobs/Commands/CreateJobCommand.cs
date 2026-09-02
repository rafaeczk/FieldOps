using FieldOps.Modules.Jobs.Application.Common;
using FieldOps.Modules.Jobs.Application.Jobs.Services;
using FieldOps.Modules.Jobs.Domain.Jobs.Entities;
using FieldOps.Modules.Jobs.Domain.Jobs.Repositories;
using FieldOps.Modules.Jobs.Domain.Jobs.ValueObjects;
using FieldOps.Modules.Jobs.Domain.Outbox;
using FieldOps.Modules.Operators.Contracts;
using FieldOps.Shared.Abstractions.Contexts;
using FieldOps.Shared.Abstractions.Messages;
using FieldOps.Shared.Abstractions.Time;

namespace FieldOps.Modules.Jobs.Application.Jobs.Commands;

public record CreateJobCommand(string Title, string? Description, JobPriority Priority, Address Address, DateTime Deadline) : IMessage<Guid>;

public sealed class CreateJobCommandHandler(IJobsRepository repository, IOutboxMessagesRepository outboxRepository, IJobsUnitOfWork unitOfWork, 
    IOperatorsModuleApi operatorsModuleApi, IEventMapper eventMapper, IContext context, IClock clock) : IMessageHandler<CreateJobCommand, Guid>
{
    public async Task<Guid> HandleAsync(CreateJobCommand message, CancellationToken ct)
    {
        var operatorId = await operatorsModuleApi.GetOperatorIdByAccountId(context.Identity.Id);

        if (operatorId is null)
            throw new UnauthorizedAccessException();

        var job = Job.Create(
            new(operatorId.Value),
            message.Title,
            message.Description,
            message.Priority,
            message.Address,
            message.Deadline,
            clock.UtcNow()
            );

        await repository.AddAsync(job);
        await outboxRepository.AddAsync([.. eventMapper.Map(job.Events)]);
        await unitOfWork.SaveChangesAsync(ct);

        return job.Id;
    }
}
