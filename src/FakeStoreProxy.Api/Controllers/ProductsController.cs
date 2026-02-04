using FakeStoreProxy.Api.Requests;
using FakeStoreProxy.Api.Models;
using FakeStoreProxy.Api.Services;
using FakeStoreProxy.Api.Helpers;
using Microsoft.AspNetCore.Mvc;

namespace FakeStoreProxy.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ProductsController(IProductsService productsService) : ControllerBase
{
    private readonly IProductsService _productsService = productsService;


    /// <summary>
    /// Searches products by name and returns a paged response.
    /// </summary>
    /// <param name="request">Query params: name, page, pageSize.</param>
    /// <param name="ct">Cancellation token.</param>
    /// <returns>Paged list of matching products.</returns>
    [HttpGet]
    [ProducesResponseType(typeof(PagedResponse<Product>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status502BadGateway)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status504GatewayTimeout)]
    public async Task<ActionResult<PagedResponse<Product>>> GetByName(
    [FromQuery] GetProductsByNameRequest request,
    CancellationToken ct = default)
    {
        try
        {
            var products = await _productsService.GetByNameAsync(request.Name, request.Page, request.PageSize, ct);

            return Ok(products);
        }
        catch (Exception ex) when (ex is HttpRequestException or TaskCanceledException)
        {
            return this.HandleUpstream(ex);
        }
    }
}
