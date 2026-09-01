using FieldOps.Modules.Reports.Domain.Reports.ValueObjects;
using System;
using System.Collections.Generic;
using System.Text;

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
