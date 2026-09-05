using FieldOps.Modules.Reports.Domain.Reports.Events;
using FieldOps.Modules.Reports.Domain.Reports.Exceptions;
using FieldOps.Shared.Abstractions.Kernel.ValueObjects;
using FieldOps.Shared.Abstractions.Kernel.Ids;
using FieldOps.Shared.Abstractions.Kernel.Types;

namespace FieldOps.Modules.Reports.Domain.Reports.Entities;

public sealed class Report : AggregateRoot
{
    public JobId JobId { get; private set; } = null!;
    public TechnicianId CreatorId { get; private set; } = null!;
    public AssetId AssetId { get; private set; } = null!;
    public string Note { get; private set; } = null!;
    public Address Address { get; private set; } = null!;
    public DateTime CreatedAt { get; private set; }
    public DateTime UpdatedAt { get; private set; }
    public bool IsDeleted { get; private set; }
    public DateTime? DeletedAt { get; private set; }
    private readonly List<FileId> _fileIds = [];
    public IReadOnlyCollection<FileId> FileIds => _fileIds.AsReadOnly();

    private Report() { }

    public static Report Create(AggregateId id, JobId jobId,  TechnicianId creatorId, AssetId assetId, string note, Address address, IEnumerable<FileId>? fileIds, DateTime createdAt)
    {
        var report = new Report
        {
            Id = id,
            JobId = jobId,
            CreatorId = creatorId,
            AssetId = assetId,
            Note = note,
            Address = address,
            CreatedAt = createdAt,
            UpdatedAt = createdAt
        };


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
    public void AddAttachment(FileId fileId)
    {
        if (!_fileIds.Contains(fileId))
        {
            _fileIds.Add(fileId);
            IncrementVersion();
        }
    }

    public void RemoveAttachment(FileId fileId)
    {
        if (_fileIds.Remove(fileId))
        {
            IncrementVersion();
        }
    }

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
}
