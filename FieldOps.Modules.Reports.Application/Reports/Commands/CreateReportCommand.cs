using FieldOps.Modules.Technicians.Contracts;
using FieldOps.Modules.Reports.Application.Common;
using FieldOps.Modules.Reports.Domain.Reports.Entities;
using FieldOps.Modules.Reports.Domain.Reports.Repositories;
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
    public record CreateReportCommand( Guid JobId,
        Guid AssetId,
        string Note,
        Address Address,
        List<Guid>? FileIds = null) : IMessage<Guid>;

    public sealed class CreateReportCommandHandler(IReportsWriteRepository repository, IReportsUnitOfWork unitOfWork, ITechniciansModuleApi technicianModuleApi, IContext context, IClock clock) : IMessageHandler<CreateReportCommand, Guid>
    {
        public async Task<Guid> HandleAsync(CreateReportCommand message, CancellationToken ct)
        {
            var operatorId = await technicianModuleApi.GetTechnicianIdByAccountId(context.Identity.Id);

            if (operatorId is null)
                throw new UnauthorizedAccessException();

            var fileIds = message.FileIds?.Select(id => new FileId(id)).ToList()
                          ?? new List<FileId>();

            var job = Report.Create(
                new AggregateId(),
                new (message.JobId),
                new(operatorId.Value),
                new(message.AssetId),
                message.Note,
                message.Address,
                fileIds,
                clock.UtcNow()
                );

            repository.Add(job);
            await unitOfWork.SaveChangesAsync(ct);

            return job.Id;
        }
    }
}
