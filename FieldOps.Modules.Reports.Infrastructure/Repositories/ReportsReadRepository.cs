using FieldOps.Modules.Reports.Application.Reports.DTOs;
using FieldOps.Modules.Reports.Application.Reports.Repositories;
using FieldOps.Modules.Reports.Domain.Reports.Entities;
using FieldOps.Shared.Abstractions.Pagination;
using FieldOps.Shared.Infrastructure.Pagination;
using Microsoft.EntityFrameworkCore;

namespace FieldOps.Modules.Reports.Infrastructure.EF.Repositories;

internal class ReportsReadRepository(ReportsDbContext context) : IReportsReadRepository
{
    private readonly ReportsDbContext context = context;

    public async Task<PagedResult<ReportListItemDto>> BrowseAsync(PaginationParams pagination)
    {
        var query = context.Reports.Where(r => !r.IsDeleted).OrderByDescending(r => r.CreatedAt);

        var totalItems = await query.CountAsync();

        var items = await context.Reports
            .Where(r => !r.IsDeleted)
            .OrderByDescending(r => r.CreatedAt)
            .Paginate(pagination)
            .Select(r => new ReportListItemDto(r.JobId, r.CreatorId, r.AssetId, r.Address.City, r.CreatedAt, r.FileIds.Count))
            .ToListAsync();

        return new(items, totalItems, pagination);
    }

    public async Task<ReportDetailsDto?> GetAsync(Guid reportId)
    {
        var report = await context.Reports
            .Include(r => r.FileIds)
            .SingleOrDefaultAsync(r => (Guid)r.Id == reportId && !r.IsDeleted);

        if (report is null) return null;

        var fileIds = report.FileIds.Select(f => f.Value).ToList();

        return new(report.JobId, report.CreatorId, report.AssetId, report.Note, report.Address, report.CreatedAt, report.UpdatedAt, fileIds);
    }

    public Task<Report?> GetByIdAsync(Guid reportId)
        => context.Reports.SingleOrDefaultAsync(r => (Guid)r.Id == reportId && !r.IsDeleted);
}
