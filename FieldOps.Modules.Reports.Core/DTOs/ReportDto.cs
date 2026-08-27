namespace FieldOps.Modules.Reports.Core.DTOs;

public class ReportDto
{
    public Guid Id { get; set; }
    public Guid WorkOrderId { get; set; }
    public Guid TechnicianId { get; set; }
    public string? Note { get; set; }
    public List<string> PhotoPaths { get; set; } = [];
    public double? Latitude { get; set; }
    public double? Longitude { get; set; }
    public string? SignaturePath { get; set; }
    public string? QrData { get; set; }
    public string Status { get; set; } = null!;
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
}
