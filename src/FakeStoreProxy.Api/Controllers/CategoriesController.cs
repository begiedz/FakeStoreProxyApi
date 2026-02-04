using FakeStoreProxy.Api.Requests;
using FakeStoreProxy.Api.Models;
using FakeStoreProxy.Api.Services;
using FakeStoreProxy.Api.Helpers;
using Microsoft.AspNetCore.Mvc;

namespace FakeStoreProxy.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class CategoriesController(IProductsService productsService) : ControllerBase
{
    private readonly IProductsService _productsService = productsService;

    /// <summary>
    /// Returns products from a given category (paged).
    /// </summary>
    /// <param name="route">Route params: category.</param>
    /// <param name="pagination">Query params: page, pageSize.</param>
    /// <param name="ct">Cancellation token.</param>
    /// <returns>Paged list of products.</returns>
    [HttpGet("{Category}/products")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(typeof(PagedResponse<Product>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status502BadGateway)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status504GatewayTimeout)]
    public async Task<ActionResult<PagedResponse<Product>>> GetByCategory(
      [FromRoute] GetProductByCategoryRoute route,
      [FromQuery] PaginationRequest pagination,
      CancellationToken ct = default)
    {

        try
        {
            var products = await _productsService.GetByCategoryAsync(route.Category, pagination.Page, pagination.PageSize, ct);

            if (products.Items.Count == 0) return NoContent();

            return Ok(products);
        }
        catch (Exception ex) when (ex is HttpRequestException or TaskCanceledException)
        {
            return this.HandleUpstream(ex);
        }
    }

}
