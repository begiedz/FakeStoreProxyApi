using System.ComponentModel.DataAnnotations;

namespace FakeStoreProxy.Api.Requests;

public sealed class GetProductsByNameRequest : PaginationRequest
{
    [Required]
    [MinLength(1)]
    [MaxLength(200)]
    public string Name { get; init; } = string.Empty;
}
