using FieldOps.Shared.Abstractions.Kernel.ValueObjects;

namespace FieldOps.Modules.Reports.Application.Reports.DTOs
{
    public record CreateReportDto(
        Guid JobId,
        Guid AssetId,
        string Note,
        Address Address,
        List<Guid>? FileIds = null
    );
}
