using FieldOps.Modules.Reports.Application.Reports.Services;
using FieldOps.Modules.Reports.Domain.Reports.Entities;
using FieldOps.Modules.Reports.Domain.Reports.Events;
using FieldOps.Shared.Abstractions.Kernel.Ids;
using FieldOps.Shared.Abstractions.Kernel.ValueObjects;

namespace FieldOps.Modules.Reports.Tests;

public class EventMapperTests
{
    [Fact]
    public void Map_ReportEvents_ToIntegrationEvents()
    {
        var mapper = new ReportEventMapper();

        var jobId = new JobId(Guid.NewGuid());
        var creator = new TechnicianId(Guid.NewGuid());
        var assetIds = new[] { new AssetId(Guid.NewGuid()) };
        var addr = new Address { CountryCode = "PL", PostalCode = "00-000", City = "W", Street = "S", BuildingNumber = "1" };
        var report = Report.Create(jobId, creator, assetIds, "Note", addr, null, DateTime.UtcNow);

        var added = new ReportAdded(report);
        var attachment = ReportAttachment.Create(new FileId(Guid.NewGuid()), new(report.Id));
        var attachmentAdded = new ReportAttachmentAdded(attachment);
        var attachmentRemoved = new ReportAttachmentRemoved(attachment);

        var mapped = mapper.Map([added, attachmentAdded, attachmentRemoved]).ToList();

        Assert.Contains(mapped, e => e is FieldOps.Modules.Reports.Contracts.Events.ReportAdded);
        Assert.Contains(mapped, e => e is FieldOps.Modules.Reports.Contracts.Events.ReportAttachmentAdded);
        Assert.Contains(mapped, e => e is FieldOps.Modules.Reports.Contracts.Events.ReportAttachmentRemoved);
    }
}
