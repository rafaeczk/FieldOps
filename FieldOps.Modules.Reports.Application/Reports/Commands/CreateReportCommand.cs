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
using FieldOps.Modules.Files.Contracts;
using FieldOps.Modules.Reports.Domain.Reports.Exceptions;
using FieldOps.Modules.Assets.Contracts;
using FieldOps.Modules.Jobs.Contracts;

namespace FieldOps.Modules.Reports.Application.Reports.Commands
{
    public record CreateReportCommand(Guid JobId,
        Guid AssetId,
        string Note,
        Address Address,
        List<Guid>? FileIds = null) : IMessage<Guid>;

    public sealed class CreateReportCommandHandler(IReportsWriteRepository repository, IReportsUnitOfWork unitOfWork, ITechniciansModuleApi technicianModuleApi, IFilesModuleApi filesModuleApi, IAssetsModuleApi assetsModuleApi,IJobsModuleApi jobsModuleApi, IContext context, IClock clock) : IMessageHandler<CreateReportCommand, Guid>
    {
        public async Task<Guid> HandleAsync(CreateReportCommand message, CancellationToken ct)
        {
            var operatorId = await technicianModuleApi.GetTechnicianIdByAccountId(context.Identity.Id);

            if (operatorId is null)
                throw new UnauthorizedAccessException();

            var jobExists = await jobsModuleApi.Exists(message.JobId, ct);
            if (!jobExists)
            {
                throw new JobNotFoundException(message.JobId);
            }

            var assetExists = await assetsModuleApi.Exists(message.AssetId, ct);
            if (!assetExists)
            {
                throw new AssetNotFoundException(message.AssetId);
            }

           

            var rawFileIds = message.FileIds?.Distinct().ToList() ?? [];
            if (rawFileIds.Count > 0 && !await filesModuleApi.AllExistAsync(rawFileIds, ct))
            {
                throw new FileDoesNotExistException();
            }

            var fileIds = rawFileIds.Select(id => new FileId(id)).ToList();

            var report = Report.Create(
                new AggregateId(),
                new(message.JobId),
                new(operatorId.Value),
                new(message.AssetId),
                message.Note,
                message.Address,
                fileIds,
                clock.UtcNow()
                );

            repository.Add(report);
            await unitOfWork.SaveChangesAsync(ct);

            return report.Id;
        }
    }
}
