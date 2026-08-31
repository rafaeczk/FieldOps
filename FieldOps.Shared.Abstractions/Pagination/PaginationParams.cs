namespace FieldOps.Shared.Abstractions.Pagination;

public record PaginationParams
{
    public PaginationParams(PaginationParams other)
    {
        PageNumber = other.PageNumber;
        PageSize = other.PageSize;
    }

    public PaginationParams(int? pageNumber, int? pageSize)
    {
        PageNumber = pageNumber ?? 1;
        PageSize = pageSize ?? 10;
    }


    public int PageNumber;
    public int PageSize;
}
