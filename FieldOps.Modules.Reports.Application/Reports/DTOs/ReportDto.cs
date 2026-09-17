using System;
using System.Collections.Generic;
using System.Text;

namespace FieldOps.Modules.Reports.Application.Reports.DTOs
{
    public record ReportListItemDto(
        Guid Id,
        Guid JobId,
        Guid CreatorId,
        List<Guid> AssetIds,
        string Note,
        string City,
        DateTime CreatedAt,
        int FilesCount,
        double? Latitude,
        double? Longitude
    );
}
