using FakeStoreProxy.Api.Services;
using Xunit;

namespace FakeStoreProxy.Tests;

public class ProductsServiceTests
{
    // simple factory that returns the same http client
    private sealed class DummyHttpClientFactory : IHttpClientFactory
    {
        private readonly HttpClient _client = new(new HttpClientHandler());
        public HttpClient CreateClient(string name) => _client;
    }

    [Fact]
    public async Task GetByNameAsync_EmptyName_ThrowsArgumentNullException()
    {
        var service = new ProductsService(new DummyHttpClientFactory());

        await Assert.ThrowsAsync<ArgumentNullException>(() =>
            service.GetByNameAsync(" ", 1, 10));
    }

    [Fact]
    public async Task GetByCategoryAsync_PageLessThanOne_ThrowsArgumentException()
    {
        var service = new ProductsService(new DummyHttpClientFactory());

        await Assert.ThrowsAsync<ArgumentException>(() =>
            service.GetByCategoryAsync("electronics", 0, 10));
    }
}
