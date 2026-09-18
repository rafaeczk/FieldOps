using FieldOps.Modules.Jobs.Application.Common;
using FieldOps.Modules.Jobs.Application.Jobs.Exceptions;
using FieldOps.Modules.Jobs.Application.Jobs.Services;
using FieldOps.Modules.Jobs.Domain.Jobs.Repositories;
using FieldOps.Modules.Jobs.Domain.Outbox;
using FieldOps.Modules.Technicians.Contracts;
using FieldOps.Shared.Abstractions.Contexts;
using FieldOps.Shared.Abstractions.Messages;

namespace FieldOps.Modules.Jobs.Application.Jobs.Commands;

public record ChangeJobStatusCommand(Guid JobId, string Status) : IMessage;

internal sealed class ChangeJobStatusCommandHandler(
    IJobsRepository repository,
    IOutboxMessagesRepository outboxRepository,
    IJobsUnitOfWork unitOfWork,
    IJobEventMapper eventMapper,
    IContext context,
    ITechnicianModuleApi technicianModuleApi) : IMessageHandler<ChangeJobStatusCommand>
{
    public async Task HandleAsync(ChangeJobStatusCommand message, CancellationToken ct)
    {
        var job = await repository.GetAsync(message.JobId);

        if (job is null)
            throw new JobNotFoundException(message.JobId);

        if (context.Identity.Role == "TECHNICIAN")
        {
            var technicianId = await technicianModuleApi.GetTechnicianIdByAccountId(context.Identity.Id);
            if (technicianId is null || !job.Assignees.Any(a => a.TechnicianId.Value == technicianId.Value))
                throw new UnauthorizedAccessException();
        }

        var status = message.Status.ToUpperInvariant();

        switch (status)
        {
            case "IN_PROGRESS":
                job.SetInProgress();
                break;
            case "COMPLETED":
                job.SetCompleted();
                break;
            case "CANCELLED":
                job.SetCancelled();
                break;
            default:
                throw new InvalidOperationException($"Invalid job status: '{status}'.");
        }

        await outboxRepository.AddAsync([.. eventMapper.Map(job.Events)]);
        await unitOfWork.SaveChangesAsync(ct);
    }
}
