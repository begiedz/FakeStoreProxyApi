using System.ComponentModel.DataAnnotations;

namespace FakeStoreProxy.Api.Requests;

public class PaginationRequest
{
    [Range(1, int.MaxValue)]
    public int Page { get; init; } = 1;

    [Range(1, 20)]
    public int PageSize { get; init; } = 10;
}
