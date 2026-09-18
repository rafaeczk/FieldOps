using FieldOps.Modules.Jobs.Application.Jobs.DTOs;
using FieldOps.Modules.Jobs.Application.Jobs.Repositories;
using FieldOps.Modules.Jobs.Application.Jobs.Specifications;
using FieldOps.Modules.Technicians.Contracts;
using FieldOps.Shared.Abstractions.Contexts;
using FieldOps.Shared.Abstractions.Messages;
using FieldOps.Shared.Abstractions.Pagination;

namespace FieldOps.Modules.Jobs.Application.Jobs.Queries;

public record BrowseJobsQuery(PaginationParams Pagination, Guid? AssigneeId = null) : IMessage<PagedResult<JobListItemDto>>;

internal sealed class BrowseJobsQueryHandler(IJobsReadRepository repository, ITechnicianModuleApi technicianModuleApi, IContext context) : IMessageHandler<BrowseJobsQuery, PagedResult<JobListItemDto>>
{
    public async Task<PagedResult<JobListItemDto>> HandleAsync(BrowseJobsQuery message, CancellationToken ct)
    {
        var technicianId = await technicianModuleApi.GetTechnicianIdByAccountId(context.Identity.Id);

        var spec = new BrowseJobsSpecification(
            context.Identity.Role,
            technicianId,
            message.Pagination,
            message.AssigneeId);

        return await repository.BrowseAsync(spec);
    }
}
