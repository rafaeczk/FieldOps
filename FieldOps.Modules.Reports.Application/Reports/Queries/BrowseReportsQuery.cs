using FieldOps.Modules.Reports.Application.Reports.DTOs;
using FieldOps.Modules.Reports.Application.Reports.Repositories;
using FieldOps.Modules.Reports.Application.Reports.Specifications;
using FieldOps.Modules.Technicians.Contracts;
using FieldOps.Shared.Abstractions.Contexts;
using FieldOps.Shared.Abstractions.Kernel.Ids;
using FieldOps.Shared.Abstractions.Messages;
using FieldOps.Shared.Abstractions.Pagination;

namespace FieldOps.Modules.Reports.Application.Reports.Queries;

public record BrowseReportsQuery(JobId? JobId, PaginationParams Pagination) : IMessage<PagedResult<ReportListItemDto>>;

internal sealed class BrowseReportsQueryHandler(IReportsReadRepository repository, ITechnicianModuleApi technicianModuleApi, IContext context) : IMessageHandler<BrowseReportsQuery, PagedResult<ReportListItemDto>>
{
    public async Task<PagedResult<ReportListItemDto>> HandleAsync(BrowseReportsQuery message, CancellationToken ct)
    {
        var technicianId = await technicianModuleApi.GetTechnicianIdByAccountId(context.Identity.Id);

        var spec = new BrowseReportsSpecification(
            context.Identity.Role,
            technicianId,
            message.JobId,
            message.Pagination);

        return await repository.BrowseAsync(message.Pagination, spec);
    }
}
