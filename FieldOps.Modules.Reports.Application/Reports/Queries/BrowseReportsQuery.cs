using FieldOps.Modules.Reports.Application.Reports.DTOs;
using FieldOps.Modules.Reports.Application.Reports.Repositories;
using FieldOps.Shared.Abstractions.Messages;
using FieldOps.Shared.Abstractions.Pagination;
using System.Linq;

namespace FieldOps.Modules.Reports.Application.Reports.Queries;

public record BrowseReportsQuery(PaginationParams Pagination) : IMessage<PagedResult<ReportListItemDto>>;

internal sealed class BrowseReportsQueryHandler(IReportsReadRepository repository) : IMessageHandler<BrowseReportsQuery, PagedResult<ReportListItemDto>>
{
    public Task<PagedResult<ReportListItemDto>> HandleAsync(BrowseReportsQuery message, CancellationToken ct)
    {
        return repository.BrowseAsync(message.Pagination);
    }
}
