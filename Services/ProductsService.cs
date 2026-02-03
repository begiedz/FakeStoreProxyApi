using FakeStoreProxyApi.Models;

namespace FakeStoreProxyApi.Services;

public class ProductsService(HttpClient httpClient)
{
    private readonly HttpClient _httpClient = httpClient;

    public async Task<List<Product>> GetByCategoryAsync()
    {
        string url = "https://fakestoreapi.com/products/";
        var res = await _httpClient.GetAsync(url);

        if ((int)res.StatusCode >= 500)
            throw new HttpRequestException($"Provider error: {(int)res.StatusCode}");

        if (res.StatusCode == System.Net.HttpStatusCode.NotFound)
            return new List<Product>();

        res.EnsureSuccessStatusCode();

        var json = await res.Content.ReadFromJsonAsync<List<Product>>();

        return json!;
    }
}
