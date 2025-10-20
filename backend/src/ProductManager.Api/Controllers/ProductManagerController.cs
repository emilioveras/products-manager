using Microsoft.AspNetCore.Mvc;
using ProductManager.Domain.Contracts;
using ProductManager.Domain.Models;

namespace ProductManager.Controllers;

[ApiController]
[ApiVersion("1.0")]
[Route("api/v{version:ApiVersion}/product-manager")]
public class ProductManagerController : ControllerBase
{
    private readonly IService _service;

    public ProductManagerController(IService service)
    {
        _service = service;
    }

    [HttpGet()]
    public async Task<IActionResult> Get([FromQuery] string? searchFilter, [FromQuery] int pageIndex = 0, [FromQuery] int pageSize = 10)
    {
        return this.Ok(await _service.GetPaginated(pageIndex, pageSize, searchFilter ?? string.Empty));
    }

    [HttpPost()]
    public async Task<IActionResult> Post([FromBody] ProductModel productModel)
    {
        return this.Ok(await _service.Add(productModel));
    }

    [HttpPut()]
    public async Task<IActionResult> Put([FromBody] ProductModel productModel)
    {
        return this.Ok(await _service.Update(productModel));
    }

    [HttpDelete()]
    public async Task<IActionResult> Delete([FromBody] ProductModel productModel)
    {
        return this.Ok(await _service.Remove(productModel));
    }
}
