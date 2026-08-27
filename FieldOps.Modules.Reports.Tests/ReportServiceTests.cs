using FieldOps.Modules.Reports.Core.DTOs;
using FieldOps.Modules.Reports.Core.Entities;
using FieldOps.Modules.Reports.Core.Exceptions;
using FieldOps.Modules.Reports.Core.Repositories;
using FieldOps.Modules.Reports.Core.Services;
using FieldOps.Shared.Abstractions.Messaging;
using FieldOps.Shared.Abstractions.Time;
using Moq;

namespace FieldOps.Modules.Reports.Tests;

public class ReportServiceTests
{
    private readonly Mock<IReportRepository> _repositoryMock = new();
    private readonly Mock<IReportUnitOfWork> _unitOfWorkMock = new();
    private readonly Mock<IMessageClient> _messageClientMock = new();
    private readonly Mock<IClock> _clockMock = new();
    private readonly ReportService _sut;

    public ReportServiceTests()
    {
        _sut = new ReportService(
            _repositoryMock.Object,
            _unitOfWorkMock.Object,
            _messageClientMock.Object,
            _clockMock.Object);
    }

    [Fact]
    public async Task CreateAsync_ValidCommand_CreatesReportAndPublishesEvent()
    {
        var dto = new CreateReportDto(Guid.NewGuid(), "Fixed the issue", 52.23, 21.01, "QR-12345");
        var technicianId = Guid.NewGuid();
        var fixedTime = new DateTime(2024, 1, 1, 0, 0, 0, DateTimeKind.Utc);

        _clockMock.Setup(x => x.UtcNow()).Returns(fixedTime);

        var result = await _sut.CreateAsync(dto, technicianId);

        Assert.NotEqual(Guid.Empty, result);
        _repositoryMock.Verify(x => x.CreateAsync(It.Is<Report>(r =>
            r.WorkOrderId == dto.WorkOrderId &&
            r.TechnicianId == technicianId &&
            r.Note == dto.Note)), Times.Once);
        _unitOfWorkMock.Verify(x => x.SaveChangesAsync(), Times.Once);
        _messageClientMock.Verify(x => x.PublishAsync(It.IsAny<Core.Events.ReportCreatedEvent>()), Times.Once);
    }

    [Fact]
    public async Task GetByAsync_ExistingReport_ReturnsReportDto()
    {
        var reportId = Guid.NewGuid();
        var report = Report.Create(Guid.NewGuid(), Guid.NewGuid(), "Test note", 52.23, 21.01, null, DateTime.UtcNow);

        _repositoryMock
            .Setup(x => x.GetAsync(reportId))
            .ReturnsAsync(report);

        var result = await _sut.GetByAsync(reportId);

        Assert.NotNull(result);
        Assert.Equal(report.Id, result.Id);
        Assert.Equal(report.Note, result.Note);
    }

    [Fact]
    public async Task GetByAsync_NonExistingReport_ReturnsNull()
    {
        _repositoryMock
            .Setup(x => x.GetAsync(It.IsAny<Guid>()))
            .ReturnsAsync((Report?)null);

        var result = await _sut.GetByAsync(Guid.NewGuid());

        Assert.Null(result);
    }

    [Fact]
    public async Task BrowseByWorkOrderAsync_ReturnsListOfReports()
    {
        var workOrderId = Guid.NewGuid();
        var reports = new List<Report>
        {
            Report.Create(workOrderId, Guid.NewGuid(), "Note 1", 52.23, 21.01, null, DateTime.UtcNow),
            Report.Create(workOrderId, Guid.NewGuid(), "Note 2", 52.24, 21.02, null, DateTime.UtcNow)
        };

        _repositoryMock
            .Setup(x => x.BrowseByWorkOrderAsync(workOrderId))
            .ReturnsAsync(reports);

        var result = await _sut.BrowseByWorkOrderAsync(workOrderId);

        Assert.Equal(2, result.Count);
    }

    [Fact]
    public async Task BrowseByTechnicianAsync_ReturnsListOfReports()
    {
        var technicianId = Guid.NewGuid();
        var reports = new List<Report>
        {
            Report.Create(Guid.NewGuid(), technicianId, "Note 1", 52.23, 21.01, null, DateTime.UtcNow),
            Report.Create(Guid.NewGuid(), technicianId, "Note 2", 52.24, 21.02, null, DateTime.UtcNow)
        };

        _repositoryMock
            .Setup(x => x.BrowseByTechnicianAsync(technicianId))
            .ReturnsAsync(reports);

        var result = await _sut.BrowseByTechnicianAsync(technicianId);

        Assert.Equal(2, result.Count);
    }

    [Fact]
    public async Task BrowsePendingSyncAsync_ReturnsPendingReports()
    {
        var technicianId = Guid.NewGuid();
        var reports = new List<Report>
        {
            Report.Create(Guid.NewGuid(), technicianId, "Note 1", 52.23, 21.01, null, DateTime.UtcNow)
        };

        _repositoryMock
            .Setup(x => x.BrowsePendingSyncAsync(technicianId))
            .ReturnsAsync(reports);

        var result = await _sut.BrowsePendingSyncAsync(technicianId);

        Assert.Single(result);
    }

    [Fact]
    public async Task AddPhotoAsync_ExistingReport_AddsPhoto()
    {
        var reportId = Guid.NewGuid();
        var report = Report.Create(Guid.NewGuid(), Guid.NewGuid(), null, null, null, null, DateTime.UtcNow);
        var fixedTime = new DateTime(2024, 1, 1, 0, 0, 0, DateTimeKind.Utc);

        _clockMock.Setup(x => x.UtcNow()).Returns(fixedTime);
        _repositoryMock
            .Setup(x => x.GetAsync(reportId))
            .ReturnsAsync(report);

        await _sut.AddPhotoAsync(reportId, "/photos/test.jpg");

        Assert.Single(report.PhotoPaths);
        Assert.Equal("/photos/test.jpg", report.PhotoPaths[0]);
        Assert.Equal(fixedTime, report.UpdatedAt);
        _unitOfWorkMock.Verify(x => x.SaveChangesAsync(), Times.Once);
    }

    [Fact]
    public async Task AddPhotoAsync_SyncedReport_ThrowsInvalidOperationException()
    {
        var reportId = Guid.NewGuid();
        var report = Report.Create(Guid.NewGuid(), Guid.NewGuid(), null, null, null, null, DateTime.UtcNow);
        report.MarkSynced(DateTime.UtcNow);

        _repositoryMock
            .Setup(x => x.GetAsync(reportId))
            .ReturnsAsync(report);

        await Assert.ThrowsAsync<InvalidOperationException>(
            () => _sut.AddPhotoAsync(reportId, "/photos/test.jpg"));
    }

    [Fact]
    public async Task AddPhotoAsync_NonExistingReport_ThrowsReportNotFoundException()
    {
        _repositoryMock
            .Setup(x => x.GetAsync(It.IsAny<Guid>()))
            .ReturnsAsync((Report?)null);

        await Assert.ThrowsAsync<ReportNotFoundException>(
            () => _sut.AddPhotoAsync(Guid.NewGuid(), "/photos/test.jpg"));
    }

    [Fact]
    public async Task SetSignatureAsync_ExistingReport_SetsSignature()
    {
        var reportId = Guid.NewGuid();
        var report = Report.Create(Guid.NewGuid(), Guid.NewGuid(), null, null, null, null, DateTime.UtcNow);
        var fixedTime = new DateTime(2024, 1, 1, 0, 0, 0, DateTimeKind.Utc);

        _clockMock.Setup(x => x.UtcNow()).Returns(fixedTime);
        _repositoryMock
            .Setup(x => x.GetAsync(reportId))
            .ReturnsAsync(report);

        await _sut.SetSignatureAsync(reportId, "/signatures/test.png");

        Assert.Equal("/signatures/test.png", report.SignaturePath);
        Assert.Equal(fixedTime, report.UpdatedAt);
        _unitOfWorkMock.Verify(x => x.SaveChangesAsync(), Times.Once);
    }

    [Fact]
    public async Task SetSignatureAsync_SyncedReport_ThrowsInvalidOperationException()
    {
        var reportId = Guid.NewGuid();
        var report = Report.Create(Guid.NewGuid(), Guid.NewGuid(), null, null, null, null, DateTime.UtcNow);
        report.MarkSynced(DateTime.UtcNow);

        _repositoryMock
            .Setup(x => x.GetAsync(reportId))
            .ReturnsAsync(report);

        await Assert.ThrowsAsync<InvalidOperationException>(
            () => _sut.SetSignatureAsync(reportId, "/signatures/test.png"));
    }

    [Fact]
    public async Task UpdateNoteAsync_ExistingReport_UpdatesNote()
    {
        var reportId = Guid.NewGuid();
        var report = Report.Create(Guid.NewGuid(), Guid.NewGuid(), "Old note", null, null, null, DateTime.UtcNow);
        var fixedTime = new DateTime(2024, 1, 1, 0, 0, 0, DateTimeKind.Utc);

        _clockMock.Setup(x => x.UtcNow()).Returns(fixedTime);
        _repositoryMock
            .Setup(x => x.GetAsync(reportId))
            .ReturnsAsync(report);

        await _sut.UpdateNoteAsync(reportId, new UpdateReportNoteDto("New note"));

        Assert.Equal("New note", report.Note);
        _unitOfWorkMock.Verify(x => x.SaveChangesAsync(), Times.Once);
    }

    [Fact]
    public async Task UpdateNoteAsync_SyncedReport_ThrowsInvalidOperationException()
    {
        var reportId = Guid.NewGuid();
        var report = Report.Create(Guid.NewGuid(), Guid.NewGuid(), null, null, null, null, DateTime.UtcNow);
        report.MarkSynced(DateTime.UtcNow);

        _repositoryMock
            .Setup(x => x.GetAsync(reportId))
            .ReturnsAsync(report);

        await Assert.ThrowsAsync<InvalidOperationException>(
            () => _sut.UpdateNoteAsync(reportId, new UpdateReportNoteDto("New note")));
    }

    [Fact]
    public async Task UpdateNoteAsync_NonExistingReport_ThrowsReportNotFoundException()
    {
        _repositoryMock
            .Setup(x => x.GetAsync(It.IsAny<Guid>()))
            .ReturnsAsync((Report?)null);

        await Assert.ThrowsAsync<ReportNotFoundException>(
            () => _sut.UpdateNoteAsync(Guid.NewGuid(), new UpdateReportNoteDto("New note")));
    }

    [Fact]
    public async Task MarkSyncedAsync_ExistingReport_MarksSyncedAndPublishesEvent()
    {
        var reportId = Guid.NewGuid();
        var report = Report.Create(Guid.NewGuid(), Guid.NewGuid(), null, null, null, null, DateTime.UtcNow);
        var fixedTime = new DateTime(2024, 1, 1, 0, 0, 0, DateTimeKind.Utc);

        _clockMock.Setup(x => x.UtcNow()).Returns(fixedTime);
        _repositoryMock
            .Setup(x => x.GetAsync(reportId))
            .ReturnsAsync(report);

        await _sut.MarkSyncedAsync(reportId);

        Assert.Equal("SYNCED", report.Status);
        _unitOfWorkMock.Verify(x => x.SaveChangesAsync(), Times.Once);
        _messageClientMock.Verify(x => x.PublishAsync(It.IsAny<Core.Events.ReportSyncedEvent>()), Times.Once);
    }

    [Fact]
    public async Task MarkSyncedAsync_NonExistingReport_ThrowsReportNotFoundException()
    {
        _repositoryMock
            .Setup(x => x.GetAsync(It.IsAny<Guid>()))
            .ReturnsAsync((Report?)null);

        await Assert.ThrowsAsync<ReportNotFoundException>(
            () => _sut.MarkSyncedAsync(Guid.NewGuid()));
    }

    [Fact]
    public async Task MarkConflictAsync_ExistingReport_MarksConflict()
    {
        var reportId = Guid.NewGuid();
        var report = Report.Create(Guid.NewGuid(), Guid.NewGuid(), null, null, null, null, DateTime.UtcNow);
        var fixedTime = new DateTime(2024, 1, 1, 0, 0, 0, DateTimeKind.Utc);

        _clockMock.Setup(x => x.UtcNow()).Returns(fixedTime);
        _repositoryMock
            .Setup(x => x.GetAsync(reportId))
            .ReturnsAsync(report);

        await _sut.MarkConflictAsync(reportId);

        Assert.Equal("CONFLICT", report.Status);
        _unitOfWorkMock.Verify(x => x.SaveChangesAsync(), Times.Once);
    }

    [Fact]
    public async Task MarkConflictAsync_NonExistingReport_ThrowsReportNotFoundException()
    {
        _repositoryMock
            .Setup(x => x.GetAsync(It.IsAny<Guid>()))
            .ReturnsAsync((Report?)null);

        await Assert.ThrowsAsync<ReportNotFoundException>(
            () => _sut.MarkConflictAsync(Guid.NewGuid()));
    }

    [Fact]
    public async Task DeleteAsync_ExistingReport_DeletesReport()
    {
        var reportId = Guid.NewGuid();
        var report = Report.Create(Guid.NewGuid(), Guid.NewGuid(), null, null, null, null, DateTime.UtcNow);

        _repositoryMock
            .Setup(x => x.GetAsync(reportId))
            .ReturnsAsync(report);

        await _sut.DeleteAsync(reportId);

        _repositoryMock.Verify(x => x.DeleteAsync(report), Times.Once);
        _unitOfWorkMock.Verify(x => x.SaveChangesAsync(), Times.Once);
    }

    [Fact]
    public async Task DeleteAsync_NonExistingReport_ThrowsReportNotFoundException()
    {
        _repositoryMock
            .Setup(x => x.GetAsync(It.IsAny<Guid>()))
            .ReturnsAsync((Report?)null);

        await Assert.ThrowsAsync<ReportNotFoundException>(
            () => _sut.DeleteAsync(Guid.NewGuid()));
    }
}
