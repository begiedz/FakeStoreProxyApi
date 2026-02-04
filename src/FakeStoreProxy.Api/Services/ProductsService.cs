using FakeStoreProxy.Api.Models;
using System.Net;

namespace FakeStoreProxy.Api.Services;

public class ProductsService(HttpClient httpClient) : IProductsService
{
    private readonly HttpClient _httpClient = httpClient;

    public async Task<PagedResponse<Product>> GetByCategoryAsync(
        string category,
        int page = 1,
        int pageSize = 10,
        CancellationToken ct = default)
    {
        if (string.IsNullOrWhiteSpace(category))
            throw new ArgumentNullException(nameof(category), "category is required.");

        ValidatePagination(page, pageSize);

        var normalizedCategory = category.Trim().ToLower();
        var encodedCategory = Uri.EscapeDataString(normalizedCategory);

        string url = $"products/category/{encodedCategory}";
        using var res = await _httpClient.GetAsync(url, ct);


        if ((int)res.StatusCode >= 500)
            throw new HttpRequestException($"Provider error: {(int)res.StatusCode}");

        if (res.StatusCode == HttpStatusCode.NotFound)
            return new PagedResponse<Product>
            {
                Items = new List<Product>(),
                Pagination = new PaginationMetadata(0, page, pageSize)
            };


        res.EnsureSuccessStatusCode();

        var all = await res.Content.ReadFromJsonAsync<List<Product>>(cancellationToken: ct)
            ?? new List<Product>();

        var totalItems = all.Count;

        var items = all
            .Skip((page - 1) * pageSize)
            .Take(pageSize)
            .ToList();

        return new PagedResponse<Product>
        {
            Items = items,
            Pagination = new PaginationMetadata(totalItems, page, pageSize)
        };

    }

    public async Task<PagedResponse<Product>> GetByNameAsync(
        string name,
        int page = 1,
        int pageSize = 10,
        CancellationToken ct = default)
    {
        if (string.IsNullOrWhiteSpace(name))
            throw new ArgumentNullException(nameof(name), "name is required.");

        ValidatePagination(page, pageSize);

        string url = $"products/";

        using var res = await _httpClient.GetAsync(url, ct);

        if ((int)res.StatusCode >= 500)
            throw new HttpRequestException($"Provider error: {(int)res.StatusCode}");

        if (res.StatusCode == HttpStatusCode.NotFound)
            return new PagedResponse<Product>
            {
                Items = new List<Product>(),
                Pagination = new PaginationMetadata(0, page, pageSize)
            };

        res.EnsureSuccessStatusCode();

        var all = await res.Content.ReadFromJsonAsync<List<Product>>(cancellationToken: ct)
            ?? new List<Product>();

        var trimmedName = name.Trim();

        var filtered = all
            .Where(p => p.Title.Contains(trimmedName, StringComparison.OrdinalIgnoreCase))
            .ToList();

        var totalItems = filtered.Count;

        var items = filtered
            .Skip((page - 1) * pageSize)
            .Take(pageSize)
            .ToList();


        return new PagedResponse<Product>
        {
            Items = items,
            Pagination = new PaginationMetadata(totalItems, page, pageSize)
        };
    }

    private static void ValidatePagination(int page, int pageSize)
    {
        if (page < 1)
            throw new ArgumentException("page must be greater than or equal to 1.", nameof(page));

        if (pageSize < 1 || pageSize > 20)
            throw new ArgumentException("pageSize must be between 1 and 20.", nameof(pageSize));
    }
}
