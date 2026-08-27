using FieldOps.Modules.Reports.Core.DTOs;
using FieldOps.Modules.Reports.Core.Entities;
using FieldOps.Modules.Reports.Core.Events;
using FieldOps.Modules.Reports.Core.Exceptions;
using FieldOps.Modules.Reports.Core.Repositories;
using FieldOps.Shared.Abstractions.Messaging;
using FieldOps.Shared.Abstractions.Time;

namespace FieldOps.Modules.Reports.Core.Services;

internal class ReportService(
    IReportRepository repository,
    IReportUnitOfWork unitOfWork,
    IMessageClient messageClient,
    IClock clock) : IReportService
{
    private readonly IReportRepository repository = repository;
    private readonly IReportUnitOfWork unitOfWork = unitOfWork;
    private readonly IMessageClient messageClient = messageClient;
    private readonly IClock clock = clock;

    public async Task<Guid> CreateAsync(CreateReportDto dto, Guid technicianId)
    {
        var report = Report.Create(
            dto.WorkOrderId,
            technicianId,
            dto.Note,
            dto.Latitude,
            dto.Longitude,
            dto.QrData,
            clock.UtcNow());

        await repository.CreateAsync(report);
        await unitOfWork.SaveChangesAsync();

        await messageClient.PublishAsync(new ReportCreatedEvent(
            report.Id,
            report.WorkOrderId,
            report.TechnicianId));

        return report.Id;
    }

    public async Task<ReportDto?> GetByAsync(Guid id)
    {
        var report = await repository.GetAsync(id);

        if (report is null)
            return null;

        return Map<ReportDto>(report);
    }

    public async Task<IReadOnlyList<ReportDto>> BrowseByWorkOrderAsync(Guid workOrderId)
    {
        var reports = await repository.BrowseByWorkOrderAsync(workOrderId);
        return [.. reports.Select(Map<ReportDto>)];
    }

    public async Task<IReadOnlyList<ReportDto>> BrowseByTechnicianAsync(Guid technicianId)
    {
        var reports = await repository.BrowseByTechnicianAsync(technicianId);
        return [.. reports.Select(Map<ReportDto>)];
    }

    public async Task<IReadOnlyList<ReportDto>> BrowsePendingSyncAsync(Guid technicianId)
    {
        var reports = await repository.BrowsePendingSyncAsync(technicianId);
        return [.. reports.Select(Map<ReportDto>)];
    }

    public async Task AddPhotoAsync(Guid reportId, string photoPath)
    {
        var report = await repository.GetAsync(reportId);

        if (report is null)
            throw new ReportNotFoundException(reportId);

        if (report.Status == "SYNCED")
            throw new InvalidOperationException("Cannot modify a synced report.");

        report.AddPhoto(photoPath, clock.UtcNow());
        await unitOfWork.SaveChangesAsync();
    }

    public async Task SetSignatureAsync(Guid reportId, string signaturePath)
    {
        var report = await repository.GetAsync(reportId);

        if (report is null)
            throw new ReportNotFoundException(reportId);

        if (report.Status == "SYNCED")
            throw new InvalidOperationException("Cannot modify a synced report.");

        report.SetSignature(signaturePath, clock.UtcNow());
        await unitOfWork.SaveChangesAsync();
    }

    public async Task UpdateNoteAsync(Guid id, UpdateReportNoteDto dto)
    {
        var report = await repository.GetAsync(id);

        if (report is null)
            throw new ReportNotFoundException(id);

        if (report.Status == "SYNCED")
            throw new InvalidOperationException("Cannot modify a synced report.");

        report.UpdateNote(dto.Note, clock.UtcNow());
        await unitOfWork.SaveChangesAsync();
    }

    public async Task MarkSyncedAsync(Guid id)
    {
        var report = await repository.GetAsync(id);

        if (report is null)
            throw new ReportNotFoundException(id);

        report.MarkSynced(clock.UtcNow());
        await unitOfWork.SaveChangesAsync();

        await messageClient.PublishAsync(new ReportSyncedEvent(
            report.Id,
            report.WorkOrderId,
            report.TechnicianId));
    }

    public async Task MarkConflictAsync(Guid id)
    {
        var report = await repository.GetAsync(id);

        if (report is null)
            throw new ReportNotFoundException(id);

        report.MarkConflict(clock.UtcNow());
        await unitOfWork.SaveChangesAsync();
    }

    public async Task DeleteAsync(Guid id)
    {
        var report = await repository.GetAsync(id);

        if (report is null)
            throw new ReportNotFoundException(id);

        await repository.DeleteAsync(report);
        await unitOfWork.SaveChangesAsync();
    }

    private static T Map<T>(Report report) where T : ReportDto, new()
        => new()
        {
            Id = report.Id,
            WorkOrderId = report.WorkOrderId,
            TechnicianId = report.TechnicianId,
            Note = report.Note,
            PhotoPaths = report.PhotoPaths,
            Latitude = report.Latitude,
            Longitude = report.Longitude,
            SignaturePath = report.SignaturePath,
            QrData = report.QrData,
            Status = report.Status,
            CreatedAt = report.CreatedAt,
            UpdatedAt = report.UpdatedAt
        };
}
