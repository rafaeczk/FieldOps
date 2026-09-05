using FieldOps.Shared.Abstractions.Kernel.ValueObjects;

namespace FieldOps.Modules.Reports.Application.Reports.DTOs
{
    public record ReportDetailsDto(
        Guid JobId,
        Guid CreatorId,
        Guid? AssetId,
        string Note,
        Address Address,
        DateTime CreatedAt,
        DateTime UpdatedAt,
        IReadOnlyCollection<Guid> FileIds
    );
}
