namespace FieldOps.Shared.Abstractions.Pagination;

public class PagedResult<T>
{
    public PagedResult()
    {
        Items = [];
    }

    public PagedResult(List<T> items, int totalItems, PaginationParams pagination)
    {
        Items = items;
        TotalPages = (int)Math.Ceiling(totalItems / (double)pagination.PageSize);
        CurrentPage = pagination.PageNumber;
    }

    public List<T> Items { get; set; }
    public int TotalPages { get; set; }
    public int CurrentPage { get; set; }
    public bool HasNext => CurrentPage < TotalPages;
    public bool HasPrevious => CurrentPage > 1;
}
