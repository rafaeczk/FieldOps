using FieldOps.Modules.Jobs.Application.Common;
using FieldOps.Modules.Jobs.Application.Jobs.Exceptions;
using FieldOps.Modules.Jobs.Domain.Jobs.Repositories;
using FieldOps.Modules.Jobs.Domain.Jobs.ValueObjects;
using FieldOps.Shared.Abstractions.Kernel.ValueObjects;
using FieldOps.Shared.Abstractions.Messages;

namespace FieldOps.Modules.Jobs.Application.Jobs.Commands;

public record EditJobCommand(Guid JobId, string Title, string? Description, JobPriority Priority, Address Address, DateTime Deadline) : IMessage;

internal sealed class EditJobCommandHandler(IJobsRepository repository, IJobsUnitOfWork unitOfWork) : IMessageHandler<EditJobCommand>
{
    public async Task HandleAsync(EditJobCommand message, CancellationToken ct)
    {
        var job = await repository.GetAsync(message.JobId);

        if (job is null)
            throw new JobNotFoundException(message.JobId);

        job.ChangeTitle(message.Title);
        job.ChangeDescription(message.Description);
        job.ChangePriority(message.Priority);
        job.ChangeAddress(message.Address);
        job.ChangeDeadline(message.Deadline);

        repository.Update(job);
        await unitOfWork.SaveChangesAsync(ct);
    }
}
