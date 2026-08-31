using FieldOps.Shared.Abstractions.Pagination;

namespace FieldOps.Shared.Infrastructure.Pagination;

public static class PaginationExtensions
{
    public static IQueryable<T> Paginate<T>(this IQueryable<T> query, PaginationParams pagination)
    {
        return query
            .Skip((pagination.PageNumber - 1) * pagination.PageSize)
            .Take(pagination.PageSize);
    }
}
