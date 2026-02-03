using Microsoft.AspNetCore.Mvc;

namespace FakeStoreProxyApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductsController : ControllerBase
    {
       
        [HttpGet]
        public IActionResult Get()
        {
            return Ok("hello");
        }
    }
}
