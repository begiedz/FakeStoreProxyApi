using FakeStoreProxyApi.Models;
using FakeStoreProxyApi.Services;
using Microsoft.AspNetCore.Mvc;

namespace FakeStoreProxyApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ProductsController(ProductsService productsService) : ControllerBase
{
    private readonly ProductsService _productsService = productsService;

    [HttpGet("by-category")]
    public async Task<ActionResult<PagedResponse<Product>>> GetByCategory(
        [FromQuery] string category,
        [FromQuery] int page = 1,
        [FromQuery] int pageSize = 10,
        CancellationToken ct = default)
    {
     
        var products = await _productsService.GetByCategoryAsync(category, page, pageSize, ct);

        if (products.Items.Count == 0)
            return NoContent();

        return Ok(products);
    }
    [HttpGet("by-name")]
    public async Task<ActionResult<PagedResponse<Product>>> GetByName(
        [FromQuery] string name,
        [FromQuery] int page = 1,
        [FromQuery] int pageSize = 10,
        CancellationToken ct = default)
    {
        var products = await _productsService.GetByNameAsync(name, page, pageSize, ct);

        if (products.Items.Count == 0)
            return NoContent();

        return Ok(products);
    }
}
