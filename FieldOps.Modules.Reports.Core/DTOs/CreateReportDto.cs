namespace FieldOps.Modules.Reports.Core.DTOs;

public record CreateReportDto(
    Guid WorkOrderId,
    string? Note,
    double? Latitude,
    double? Longitude,
    string? QrData);
