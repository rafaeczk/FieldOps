using FieldOps.Modules.Reports.Domain.Reports.ValueObjects;
using FieldOps.Shared.Abstractions.Contexts;
using FieldOps.Shared.Abstractions.Kernel.Ids;
using FieldOps.Shared.Abstractions.Kernel.Types;
using FieldOps.Shared.Abstractions.Messages;
using FieldOps.Shared.Abstractions.Time;
using System;
using System.Collections.Generic;
using System.Text;

namespace FieldOps.Modules.Reports.Application.Reports.Commands
{
    public record CreateReportCommandHandler(TechnicianId CreatorId, Guid  ) : IMessage<Guid>;

    public sealed class CreateJobCommandHandler(IJobsRepository repository, IJobsUnitOfWork unitOfWork, IOperatorsModuleApi operatorsModuleApi, IContext context, IClock clock) : IMessageHandler<CreateJobCommand, Guid>
    {
        public async Task<Guid> HandleAsync(CreateJobCommand message, CancellationToken ct)
        {
            var operatorId = await operatorsModuleApi.GetOperatorIdByAccountId(context.Identity.Id);

            if (operatorId is null)
                throw new UnauthorizedAccessException();

            var job = Job.Create(
                new AggregateId(),
                new(operatorId.Value),
                message.Title,
                message.Description,
                message.Priority,
                message.Address,
                message.Deadline,
                clock.UtcNow()
                );

            repository.Add(job);
            await unitOfWork.SaveChangesAsync(ct);

            return job.Id;
        }
    }
}
