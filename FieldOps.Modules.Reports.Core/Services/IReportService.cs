using FieldOps.Modules.Reports.Core.DTOs;

namespace FieldOps.Modules.Reports.Core.Services;

public interface IReportService
{
    Task<Guid> CreateAsync(CreateReportDto dto, Guid technicianId);
    Task<ReportDto?> GetByAsync(Guid id);
    Task<IReadOnlyList<ReportDto>> BrowseByWorkOrderAsync(Guid workOrderId);
    Task<IReadOnlyList<ReportDto>> BrowseByTechnicianAsync(Guid technicianId);
    Task<IReadOnlyList<ReportDto>> BrowsePendingSyncAsync(Guid technicianId);
    Task AddPhotoAsync(Guid reportId, string photoPath);
    Task SetSignatureAsync(Guid reportId, string signaturePath);
    Task UpdateNoteAsync(Guid id, UpdateReportNoteDto dto);
    Task MarkSyncedAsync(Guid id);
    Task MarkConflictAsync(Guid id);
    Task DeleteAsync(Guid id);
}
