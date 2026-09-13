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

    public async Task<PagedResult<ReportListItemDto>> BrowseAsync(PaginationParams pagination, BrowseReportsSpecification spec)
    {
        var query = SpecificationEvaluator.GetQuery(context.Reports.AsQueryable(), spec);

        var totalItems = await query.CountAsync();

        var items = await query
            .Select(r => new ReportListItemDto(r.Id, r.JobId, r.CreatorId, r.AssetId, r.Address.City, r.CreatedAt, r.Attachments.Count))
            .ToListAsync();

        return new(items, totalItems, pagination);
    }

    public async Task<ReportDetailsDto?> GetAsync(GetReportSpecification spec)
    {
        var report = await SpecificationEvaluator.GetQuery(context.Reports.AsQueryable(), spec).SingleOrDefaultAsync();

        if (report is null) return null;

        var fileIds = report.Attachments.Select(a => a.FileId.Value).ToList();

        return new(report.Id, report.Version, report.JobId, report.CreatorId, report.AssetId, report.Note, report.Address, report.CreatedAt, report.UpdatedAt, fileIds);
    }

    public Task<Report?> GetByIdAsync(Guid reportId)
        => context.Reports.SingleOrDefaultAsync(r => (Guid)r.Id == reportId && !r.IsDeleted);
}
