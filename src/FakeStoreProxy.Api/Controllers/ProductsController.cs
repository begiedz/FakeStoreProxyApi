using FakeStoreProxy.Api.Requests;
using FakeStoreProxy.Api.Models;
using FakeStoreProxy.Api.Services;
using Microsoft.AspNetCore.Mvc;

namespace FakeStoreProxy.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ProductsController(IProductsService productsService) : ControllerBase
{
    private readonly IProductsService _productsService = productsService;

    [HttpGet]
    public async Task<ActionResult<PagedResponse<Product>>> GetByName(
    [FromQuery] GetProductsByNameRequest request,
    CancellationToken ct = default)
    {
        var products = await _productsService.GetByNameAsync(request.Name, request.Page, request.PageSize, ct);

        return Ok(products);
    }
}
