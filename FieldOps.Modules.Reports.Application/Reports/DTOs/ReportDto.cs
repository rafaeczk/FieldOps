using System;
using System.Collections.Generic;
using System.Text;

namespace FieldOps.Modules.Reports.Application.Reports.DTOs
{
    public record ReportListItemDto(
        Guid JobId,
        Guid CreatorId,
        Guid? AssetId,
        string City,      
        DateTime CreatedAt,
        int FilesCount   
    );
}
