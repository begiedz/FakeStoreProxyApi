using System.ComponentModel.DataAnnotations;

namespace FakeStoreProxy.Api.Models;

public sealed class PagedResponse<T>
{
    [Required]
    public required List<T> Items { get; init; } = [];

    [Required]
    public required PaginationMetadata Pagination { get; init; } = default!;
}
