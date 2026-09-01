using FieldOps.Modules.Reports.Application.Reports.DTOs;
using FieldOps.Modules.Reports.Domain.Reports.Entities;
using FieldOps.Shared.Abstractions.Kernel.Types;
using FieldOps.Shared.Abstractions.Pagination;

namespace FieldOps.Modules.Reports.Application.Reports.Repositories;

public interface IReportReadRepository
{
    Task<PagedResult<Report>> BrowseAsync(PaginationParams pagination);
    Task<ReportDetailsDto?> GetAsync(Guid jobId);
}
