using FieldOps.Shared.Abstractions.Kernel.ValueObjects;

namespace FieldOps.Modules.Reports.Application.Reports.DTOs
{
    public record ReportDetailsDto(
        Guid Id,
        int Version,
        Guid JobId,
        Guid CreatorId,
        List<Guid> AssetIds,
        string Note,
        Address Address,
        double? Latitude,
        double? Longitude,
        Guid? SignatureFileId,
        DateTime CreatedAt,
        DateTime UpdatedAt,
        IReadOnlyCollection<Guid> FileIds
    );
}
