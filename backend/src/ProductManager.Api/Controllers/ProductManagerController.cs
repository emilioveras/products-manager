using Microsoft.AspNetCore.Mvc;
using ProductManager.Domain.Contracts;
using ProductManager.Domain.Models;

namespace ProductManager.Controllers;

[ApiController]
[ApiVersion("1.0")]
[Route("api/v{version:ApiVersion}/product-manager")]
public class ProductManagerController : ControllerBase
{
    private readonly IServiceProduct _service;

    public ProductManagerController(IServiceProduct service)
    {
        _service = service;
    }

    [HttpGet()]
    public async Task<IActionResult> Get([FromQuery] string? searchFilter, [FromQuery] int pageIndex = 0, [FromQuery] int pageSize = 10)
    {
        return this.Ok(await _service.GetPaginated(pageIndex, pageSize, searchFilter ?? string.Empty));
    }
}
