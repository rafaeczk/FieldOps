using FieldOps.Modules.Reports.Application.Reports.DTOs;
using FieldOps.Modules.Reports.Application.Reports.Repositories;
using FieldOps.Modules.Reports.Application.Reports.Specifications;
using FieldOps.Modules.Reports.Domain.Reports.Entities;
using FieldOps.Shared.Abstractions.Pagination;
using FieldOps.Shared.Infrastructure.Queries;
using Microsoft.EntityFrameworkCore;

namespace FieldOps.Modules.Reports.Infrastructure.EF.Repositories;

internal class ReportsReadRepository(ReportsDbContext context) : IReportsReadRepository
{
    private readonly ReportsDbContext context = context;

    public async Task<PagedResult<ReportListItemDto>> BrowseAsync(BrowseReportsSpecification spec)
    {
        var query = SpecificationEvaluator.GetQuery(context.Reports.AsNoTracking(), spec);

        var totalItems = await SpecificationEvaluator.GetCountQuery(context.Reports.AsNoTracking(), spec).CountAsync();

        var items = await query
            .Select(r => new ReportListItemDto(
                r.Id,
                r.JobId,
                r.CreatorId,
                r.ReportAssets.Select(a => a.AssetId.Value).ToList(),
                r.Note,
                r.Address.City,
                r.CreatedAt,
                r.Attachments.Count,
                r.Latitude,
                r.Longitude))
            .ToListAsync();

        return new(items, totalItems, spec.PaginationParams!);
    }

    public async Task<ReportDetailsDto?> GetAsync(GetReportSpecification spec)
    {
        var report = await SpecificationEvaluator.GetQuery(context.Reports.Include(r => r.Attachments).AsNoTracking(), spec).SingleOrDefaultAsync();

        if (report is null) return null;

        var fileIds = report.Attachments.Select(a => a.FileId.Value).ToList();
        var assetIds = report.ReportAssets.Select(a => a.AssetId.Value).ToList();

        return new(
            report.Id,
            report.Version,
            report.JobId,
            report.CreatorId,
            assetIds,
            report.Note,
            report.Address,
            report.Latitude,
            report.Longitude,
            report.SignatureFileId?.Value,
            report.CreatedAt,
            report.UpdatedAt,
            fileIds);
    }

    public Task<Report?> GetByIdAsync(Guid reportId)
        => context.Reports.SingleOrDefaultAsync(r => (Guid)r.Id == reportId && !r.IsDeleted);
}
