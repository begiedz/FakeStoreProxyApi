using FakeStoreProxy.Api.Requests;
using FakeStoreProxy.Api.Models;
using FakeStoreProxy.Api.Services;
using Microsoft.AspNetCore.Mvc;

namespace FakeStoreProxy.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class CategoriesController(IProductsService productsService) : ControllerBase
{
    private readonly IProductsService _productsService = productsService;

    [HttpGet("{Category}/products")]
    public async Task<ActionResult<PagedResponse<Product>>> GetByCategory(
      [FromRoute] GetProductByCategoryRoute route,
      [FromQuery] PaginationRequest pagination,
      CancellationToken ct = default)
    {
        var products = await _productsService.GetByCategoryAsync(
            route.Category, pagination.Page, pagination.PageSize, ct);

        return Ok(products);
    }

}
