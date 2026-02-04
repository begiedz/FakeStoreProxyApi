namespace FakeStoreProxyApi.Models;

public sealed class PaginationMetadata(int totalItems, int currentPage, int pageSize)
{
    public int TotalItems { get; } = totalItems;
    public int CurrentPage { get; } = currentPage;
    public int PageSize { get; } = pageSize;
    // it rounds up (Math.Ceiling) to include rest of division as next page, pageSize is cast to double to force division result to this type
    public int TotalPages { get; } = totalItems == 0 ? 0 : (int)Math.Ceiling(totalItems / (double)pageSize);
}
