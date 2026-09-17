using FieldOps.Modules.Reports.Domain.Reports.Entities;
using FieldOps.Modules.Reports.Domain.Reports.Exceptions;
using FieldOps.Shared.Abstractions.Kernel.Ids;
using FieldOps.Shared.Abstractions.Kernel.ValueObjects;

namespace FieldOps.Modules.Reports.Tests;

public class ReportTests
{
    [Fact]
    public void Create_WithValidData_CreatesReportAndAddsEvent()
    {
        var jobId = new JobId(Guid.NewGuid());
        var creator = new TechnicianId(Guid.NewGuid());
        var assetIds = new[] { new AssetId(Guid.NewGuid()) };
        var addr = new Address { CountryCode = "PL", PostalCode = "00-000", City = "Warsaw", Street = "Main", BuildingNumber = "1" };

        var report = Report.Create(jobId, creator, assetIds, "Note", addr, null, DateTime.UtcNow);

        Assert.NotNull(report.Id);
        Assert.Equal(creator, report.CreatorId);
        Assert.Equal("Note", report.Note);
        Assert.NotEmpty(report.Events);
    }

    [Fact]
    public void ChangeNote_WithEmpty_Throws()
    {
        var jobId = new JobId(Guid.NewGuid());
        var creator = new TechnicianId(Guid.NewGuid());
        var addr = new Address { CountryCode = "PL", PostalCode = "00-000", City = "Warsaw", Street = "Main", BuildingNumber = "1" };

        var report = Report.Create(jobId, creator, null, "Initial", addr, null, DateTime.UtcNow);

        Assert.Throws<EmptyReportNoteException>(() => report.ChangeNote(" "));
    }

    [Fact]
    public void AddAttachment_Duplicate_Throws()
    {
        var jobId = new JobId(Guid.NewGuid());
        var creator = new TechnicianId(Guid.NewGuid());
        var addr = new Address { CountryCode = "PL", PostalCode = "00-000", City = "Warsaw", Street = "Main", BuildingNumber = "1" };

        var report = Report.Create(jobId, creator, null, "Note", addr, null, DateTime.UtcNow);
        var fileId = new FileId(Guid.NewGuid());

        report.AddAttachment(fileId);

        Assert.Throws<AttachmentAlreadyExists>(() => report.AddAttachment(fileId));
    }

    [Fact]
    public void RemoveAttachment_NotExists_Throws()
    {
        var jobId = new JobId(Guid.NewGuid());
        var creator = new TechnicianId(Guid.NewGuid());
        var addr = new Address { CountryCode = "PL", PostalCode = "00-000", City = "Warsaw", Street = "Main", BuildingNumber = "1" };

        var report = Report.Create(jobId, creator, null, "Note", addr, null, DateTime.UtcNow);

        Assert.Throws<FileNotFoundInReportException>(() => report.RemoveAttachment(new FileId(Guid.NewGuid())));
    }

    [Fact]
    public void AddAsset_TooMany_Throws()
    {
        var jobId = new JobId(Guid.NewGuid());
        var creator = new TechnicianId(Guid.NewGuid());
        var addr = new Address { CountryCode = "PL", PostalCode = "00-000", City = "Warsaw", Street = "Main", BuildingNumber = "1" };

        var report = Report.Create(jobId, creator, null, "Note", addr, null, DateTime.UtcNow);

        for (var i = 0; i < 10; i++)
        {
            report.AddAsset(new AssetId(Guid.NewGuid()));
        }

        Assert.Throws<TooManyReportAssetsException>(() => report.AddAsset(new AssetId(Guid.NewGuid())));
    }

    [Fact]
    public void AddAsset_Duplicate_Ignores()
    {
        var jobId = new JobId(Guid.NewGuid());
        var creator = new TechnicianId(Guid.NewGuid());
        var addr = new Address { CountryCode = "PL", PostalCode = "00-000", City = "Warsaw", Street = "Main", BuildingNumber = "1" };
        var assetId = new AssetId(Guid.NewGuid());

        var report = Report.Create(jobId, creator, new[] { assetId }, "Note", addr, null, DateTime.UtcNow);
        report.AddAsset(assetId);

        Assert.Single(report.ReportAssets);
    }
}
