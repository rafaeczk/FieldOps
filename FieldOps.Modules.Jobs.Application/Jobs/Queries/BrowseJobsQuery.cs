using FieldOps.Modules.Jobs.Application.Jobs.DTOs;
using FieldOps.Modules.Jobs.Application.Jobs.Repositories;
using FieldOps.Shared.Abstractions.Messages;
using FieldOps.Shared.Abstractions.Pagination;

namespace FieldOps.Modules.Jobs.Application.Jobs.Queries;

public record BrowseJobsQuery(PaginationParams Pagination) : IMessage<PagedResult<JobListItemDto>>;

internal sealed class BrowseJobsQueryHandler(IJobsReadRepository repository) : IMessageHandler<BrowseJobsQuery, PagedResult<JobListItemDto>>
{
    public Task<PagedResult<JobListItemDto>> HandleAsync(BrowseJobsQuery message, CancellationToken ct)
    {
        return repository.BrowseAsync(message.Pagination);
    }
}
