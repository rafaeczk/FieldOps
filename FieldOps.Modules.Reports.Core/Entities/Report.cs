using FieldOps.Modules.Reports.Core.ValueObjects;

namespace FieldOps.Modules.Reports.Core.Entities;

internal class Report
{
    public Guid Id { get; private set; }
    public Guid WorkOrderId { get; private set; }
    public Guid TechnicianId { get; private set; }
    public string? Note { get; private set; }
    public List<string> PhotoPaths { get; private set; } = [];
    public double? Latitude { get; private set; }
    public double? Longitude { get; private set; }
    public string? SignaturePath { get; private set; }
    public string? QrData { get; private set; }
    public SyncStatus Status { get; private set; } = null!;
    public DateTime CreatedAt { get; private set; }
    public DateTime UpdatedAt { get; private set; }

    private Report() { }

    public static Report Create(
        Guid workOrderId,
        Guid technicianId,
        string? note,
        double? latitude,
        double? longitude,
        string? qrData,
        DateTime createdAt)
    {
        return new Report
        {
            Id = Guid.NewGuid(),
            WorkOrderId = workOrderId,
            TechnicianId = technicianId,
            Note = note,
            Latitude = latitude,
            Longitude = longitude,
            QrData = qrData,
            Status = new SyncStatus(SyncStatus.Pending),
            CreatedAt = createdAt,
            UpdatedAt = createdAt
        };
    }

    public void AddPhoto(string photoPath, DateTime updatedAt)
    {
        PhotoPaths = [.. PhotoPaths, photoPath];
        UpdatedAt = updatedAt;
    }

    public void SetSignature(string signaturePath, DateTime updatedAt)
    {
        SignaturePath = signaturePath;
        UpdatedAt = updatedAt;
    }

    public void UpdateNote(string? note, DateTime updatedAt)
    {
        Note = note;
        UpdatedAt = updatedAt;
    }

    public void MarkSynced(DateTime updatedAt)
    {
        Status = new SyncStatus(SyncStatus.Synced);
        UpdatedAt = updatedAt;
    }

    public void MarkConflict(DateTime updatedAt)
    {
        Status = new SyncStatus(SyncStatus.Conflict);
        UpdatedAt = updatedAt;
    }
}
