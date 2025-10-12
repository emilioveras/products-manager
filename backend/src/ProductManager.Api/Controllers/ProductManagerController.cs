using Microsoft.AspNetCore.Mvc;

namespace ProductManager.Controllers;

[ApiController]
[Route("[controller]")]
public class ProductManagerController : ControllerBase
{
    private readonly ILogger<ProductManagerController> _logger;

    public ProductManagerController(ILogger<ProductManagerController> logger)
    {
        _logger = logger;
    }

    [HttpGet(Name = "GetWeatherForecast")]
    public string Get()
    {
        return "Hello World";
    }
}
