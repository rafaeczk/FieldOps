using FieldOps.Modules.Reports.Domain.Reports.ValueObjects;
using System;
using System.Collections.Generic;
using System.Text;

namespace FieldOps.Modules.Reports.Application.DTOs
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
