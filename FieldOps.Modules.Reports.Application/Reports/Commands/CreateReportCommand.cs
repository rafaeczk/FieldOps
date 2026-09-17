using FieldOps.Modules.Technicians.Contracts;
using FieldOps.Modules.Reports.Application.Common;
using FieldOps.Modules.Reports.Domain.Reports.Entities;
using FieldOps.Modules.Reports.Domain.Reports.Repositories;
using FieldOps.Shared.Abstractions.Kernel.ValueObjects;
using FieldOps.Shared.Abstractions.Contexts;
using FieldOps.Shared.Abstractions.Kernel.Ids;
using FieldOps.Shared.Abstractions.Messages;
using FieldOps.Shared.Abstractions.Time;
using FieldOps.Modules.Files.Contracts;
using FieldOps.Modules.Reports.Domain.Reports.Exceptions;
using FieldOps.Modules.Assets.Contracts;
using FieldOps.Modules.Jobs.Contracts;

namespace FieldOps.Modules.Reports.Application.Reports.Commands;

public record CreateReportCommand(
    Guid JobId,
    Address Address,
    string Note = "",
    List<Guid>? AssetIds = null,
    List<Guid>? FileIds = null,
    double? Latitude = null,
    double? Longitude = null,
    Guid? SignatureFileId = null) : IMessage<Guid>;

public sealed class CreateReportCommandHandler(IReportsWriteRepository repository, IReportsUnitOfWork unitOfWork, ITechnicianModuleApi technicianModuleApi, IFilesModuleApi filesModuleApi, IAssetsModuleApi assetsModuleApi, IJobsModuleApi jobsModuleApi, IContext context, IClock clock) : IMessageHandler<CreateReportCommand, Guid>
{
    public async Task<Guid> HandleAsync(CreateReportCommand message, CancellationToken ct)
    {
        var operatorId = await technicianModuleApi.GetTechnicianIdByAccountId(context.Identity.Id);

        if (operatorId is null)
            throw new TechnicianNotFoundException(context.Identity.Id);

        var jobExists = await jobsModuleApi.Exists(message.JobId, ct);
        if (!jobExists)
        {
            throw new JobNotFoundException(message.JobId);
        }

        List<AssetId>? assetIds = null;
        if (message.AssetIds is { Count: > 0 })
        {
            var validAssetIds = await assetsModuleApi.ExistsMany(message.AssetIds, ct);
            if (validAssetIds.Count != message.AssetIds.Count)
            {
                var invalidIds = message.AssetIds.Except(validAssetIds);
                throw new AssetNotFoundException(invalidIds.First());
            }
            assetIds = validAssetIds.Select(id => new AssetId(id)).ToList();
        }

        var rawFileIds = message.FileIds?.Distinct().ToList() ?? [];
        if (rawFileIds.Count > 0 && !await filesModuleApi.AllExistAsync(rawFileIds, ct))
        {
            throw new Domain.Reports.Exceptions.FileNotFoundException();
        }

        var fileIds = rawFileIds.Select(id => new FileId(id)).ToList();

        var signatureFileId = message.SignatureFileId.HasValue ? new FileId(message.SignatureFileId.Value) : null;

        var report = Report.Create(
            new(message.JobId),
            new(operatorId.Value),
            assetIds,
            message.Note,
            message.Address,
            fileIds,
            message.Latitude,
            message.Longitude,
            signatureFileId,
            clock.UtcNow()
            );

        repository.Add(report);
        await unitOfWork.SaveChangesAsync(ct);

        return report.Id;
    }
}
