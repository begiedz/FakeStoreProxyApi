using FakeStoreProxyApi.Models;
using Microsoft.AspNetCore.Http.Features;
using System.Net;

namespace FakeStoreProxyApi.Services;

public class ProductsService(HttpClient httpClient)
{
    private readonly HttpClient _httpClient = httpClient;

    public async Task<PagedResponse<Product>> GetByCategoryAsync(
        string category,
        int page = 1,
        int pageSize = 10,
        CancellationToken ct = default)
    {
        if (string.IsNullOrWhiteSpace(category))
            throw new ArgumentNullException(nameof(category), "category is required");

        if (page < 1)
            throw new ArgumentException(nameof(page), "page must be grater than, or equals 1");

        if (pageSize < 1)
            throw new ArgumentException(nameof(pageSize), "pageSize must be  grater than, or equals 1");


        var trimmedCategory = category.Trim();
        var encodedCategory = Uri.EscapeDataString(trimmedCategory);

        string url = $"products/category/{encodedCategory}";
        var res = await _httpClient.GetAsync(url, ct);

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
            throw new ArgumentNullException(nameof(name), "name is required");

        if (page < 1)
            throw new ArgumentException(nameof(page), "page must be grater than, or equals 1");

        if (pageSize < 1)
            throw new ArgumentException(nameof(pageSize), "pageSize must be  grater than, or equals 1");


        string url = $"products/";

        var res = await _httpClient.GetAsync(url, ct);

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

        var filtered = all
            .Where(p => p.Title.Contains(name.Trim()))
            .ToList();

        var totalItems = filtered.Count;

        return new PagedResponse<Product> 
        {
            Items = filtered,
            Pagination = new PaginationMetadata(totalItems, page, pageSize)
        };
    }
}
