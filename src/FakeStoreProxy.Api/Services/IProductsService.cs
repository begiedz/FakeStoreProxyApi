using FakeStoreProxy.Api.Models;

namespace FakeStoreProxy.Api.Services;

public interface IProductsService
{
    Task<PagedResponse<Product>> GetByNameAsync(string name, int page, int pageSize, CancellationToken ct = default);
    Task<PagedResponse<Product>> GetByCategoryAsync(string category, int page, int pageSize, CancellationToken ct = default);
}
