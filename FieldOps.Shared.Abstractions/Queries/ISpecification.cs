using FieldOps.Shared.Abstractions.Pagination;
using System.Linq.Expressions;

namespace FieldOps.Shared.Abstractions.Queries;

public interface ISpecification<T>
{
    Expression<Func<T, bool>>? Criteria { get; }
    List<Expression<Func<T, object>>> Includes { get; }
    Expression<Func<T, object>>? OrderBy { get; }
    Expression<Func<T, object>>? OrderByDescending { get; }
    PaginationParams? PaginationParams { get; }
    bool IsPagingEnabled { get; }
}
