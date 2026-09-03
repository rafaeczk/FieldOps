using FieldOps.Modules.Jobs.Application.Common;
using FieldOps.Modules.Jobs.Application.Jobs.Exceptions;
using FieldOps.Modules.Jobs.Application.Jobs.Services;
using FieldOps.Modules.Jobs.Domain.Jobs.Repositories;
using FieldOps.Modules.Jobs.Domain.Outbox;
using FieldOps.Modules.Technicians.Contracts;
using FieldOps.Shared.Abstractions.Kernel.Ids;
using FieldOps.Shared.Abstractions.Messages;

namespace FieldOps.Modules.Jobs.Application.Jobs.Commands;

public record AddJobAssigneeCommand(JobId JobId, TechnicianId TechnicianId) : IMessage;

internal sealed class AddJobAssigneeCommandHandler(IJobsRepository repository, IOutboxMessagesRepository outboxRepository, IJobsUnitOfWork unitOfWork,
    IJobEventMapper eventMapper, ITechnicianModuleApi technicianModuleApi) : IMessageHandler<AddJobAssigneeCommand>
{
    public async Task HandleAsync(AddJobAssigneeCommand message, CancellationToken ct)
    {
        var job = await repository.GetAsync(message.JobId);

        if (job is null)
            throw new JobNotFoundException(message.JobId);

        if (!await technicianModuleApi.GetTechnicianExists(message.TechnicianId))
            throw new TechnicianNotFoundException(message.TechnicianId);

        job.AddAssignee(message.TechnicianId);
        await outboxRepository.AddAsync([.. eventMapper.Map(job.Events)]);
        await unitOfWork.SaveChangesAsync(ct);
    }
}
