using FieldOps.Modules.Reports.Application.Reports.DTOs;
using FieldOps.Modules.Reports.Application.Reports.Specifications;
using FieldOps.Modules.Reports.Domain.Reports.Entities;
using FieldOps.Shared.Abstractions.Pagination;

namespace FieldOps.Modules.Reports.Application.Reports.Repositories;

public interface IReportsReadRepository
{
    Task<PagedResult<ReportListItemDto>> BrowseAsync(BrowseReportsSpecification spec);
    Task<ReportDetailsDto?> GetAsync(GetReportSpecification spec);
    Task<Report?> GetByIdAsync(Guid reportId);
}
