using FieldOps.Shared.Abstractions.Kernel.ValueObjects;

namespace FieldOps.Modules.Reports.Application.Reports.DTOs
{
    public record CreateReportDto(
        Guid JobId,
        Address Address,
        string Note = "",
        List<Guid>? AssetIds = null,
        List<Guid>? FileIds = null,
        double? Latitude = null,
        double? Longitude = null,
        Guid? SignatureFileId = null
    );
}
