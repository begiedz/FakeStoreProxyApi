using System.ComponentModel.DataAnnotations;

namespace FakeStoreProxy.Api.Requests;

public class GetProductByCategoryRequest: PaginationRequest
{
    [Required]
    [MinLength(1)]
    [MaxLength(200)]
    public string Category { get; init; } = string.Empty;
}
