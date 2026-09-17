using FieldOps.Modules.Reports.Domain.Reports.Events;
using FieldOps.Modules.Reports.Domain.Reports.Exceptions;
using FieldOps.Shared.Abstractions.Kernel.ValueObjects;
using FieldOps.Shared.Abstractions.Kernel.Ids;
using FieldOps.Shared.Abstractions.Kernel.Types;

namespace FieldOps.Modules.Reports.Domain.Reports.Entities;

public sealed class Report : AggregateRoot<ReportId>
{
    private const int MaxAssets = 10;

    public JobId JobId { get; private set; } = null!;
    public TechnicianId CreatorId { get; private set; } = null!;
    public string Note { get; private set; } = null!;
    public Address Address { get; private set; } = null!;
    public double? Latitude { get; private set; }
    public double? Longitude { get; private set; }
    public FileId? SignatureFileId { get; private set; }
    public DateTime CreatedAt { get; private set; }
    public DateTime UpdatedAt { get; private set; }
    public bool IsDeleted { get; private set; }
    public DateTime? DeletedAt { get; private set; }
    private readonly List<ReportAttachment> _attachments = [];
    public IReadOnlyCollection<ReportAttachment> Attachments => _attachments.AsReadOnly();
    private readonly List<ReportAsset> _reportAssets = [];
    public IReadOnlyCollection<ReportAsset> ReportAssets => _reportAssets.AsReadOnly();

    private Report() { }

    public static Report Create(JobId jobId, TechnicianId creatorId, IEnumerable<AssetId>? assetIds, string note, Address address, IEnumerable<FileId>? fileIds,
        double? latitude, double? longitude, FileId? signatureFileId, DateTime createdAt)
    {
        var report = new Report
        {
            Id = Guid.NewGuid(),
            JobId = jobId,
            CreatorId = creatorId,
            Note = note,
            Address = address,
            Latitude = latitude,
            Longitude = longitude,
            SignatureFileId = signatureFileId,
            CreatedAt = createdAt,
            UpdatedAt = createdAt
        };

        if (assetIds is not null)
        {
            foreach (var assetId in assetIds)
            {
                report.AddAsset(assetId);
            }
        }

        if (fileIds is not null)
        {
            foreach (var fileId in fileIds)
            {
                report.AddAttachment(fileId);
            }
        }

        report.IncrementVersion();
        report.AddEvent(new ReportAdded(report));

        return report;
    }

    public static Report Create(JobId jobId, TechnicianId creatorId, IEnumerable<AssetId>? assetIds, string note, Address address, IEnumerable<FileId>? fileIds, DateTime createdAt)
        => Create(jobId, creatorId, assetIds, note, address, fileIds, null, null, null, createdAt);

    public void ChangeNote(string note)
    {
        if (string.IsNullOrWhiteSpace(note))
            throw new EmptyReportNoteException(Id);
        Note = note;
        UpdatedAt = DateTime.UtcNow;
        IncrementVersion();
    }

    public void ChangeAddress(Address address)
    {
        Address = address;
        UpdatedAt = DateTime.UtcNow;
        IncrementVersion();
    }

    public void SoftDelete()
    {
        if (IsDeleted) return;
        IsDeleted = true;
        DeletedAt = DateTime.UtcNow;
        UpdatedAt = DateTime.UtcNow;
        IncrementVersion();
    }

    public void AddAsset(AssetId assetId)
    {
        if (_reportAssets.Count >= MaxAssets)
            throw new TooManyReportAssetsException();

        if (_reportAssets.Any(a => a.AssetId == assetId))
            return;

        var reportAsset = ReportAsset.Create(assetId, new(Id));
        _reportAssets.Add(reportAsset);
    }

    public void RemoveAsset(AssetId assetId)
    {
        var reportAsset = _reportAssets.SingleOrDefault(a => a.AssetId == assetId);
        if (reportAsset is not null)
            _reportAssets.Remove(reportAsset);
    }

    public void AddAttachment(FileId fileId)
    {
        if (_attachments.Any(a => a.FileId == fileId))
            throw new AttachmentAlreadyExists(Id, fileId);

        var attachment = ReportAttachment.Create(fileId, new(Id));
        _attachments.Add(attachment);
        AddEvent(new ReportAttachmentAdded(attachment));
    }

    public void RemoveAttachment(FileId fileId)
    {
        var attachment = _attachments.SingleOrDefault(a => a.FileId == fileId);

        if (attachment is null)
            throw new FileNotFoundInReportException(Id, fileId);

        _attachments.Remove(attachment);
        AddEvent(new ReportAttachmentRemoved(attachment));
    }
}
