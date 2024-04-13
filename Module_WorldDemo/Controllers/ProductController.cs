using ClassLibrary_DataAccess.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Module_WorldDemo.Controllers;

[ApiController]
[Route("api/dataAccess/[controller]")]
public class ProductController : ControllerBase
{
    private readonly ISender _sender;
    public ProductController(ISender sender)
    {
        _sender = sender;
    }

    [HttpGet]
    public async Task<IActionResult> GetAllProducts()
    {
        var products = await _sender.Send(new GetProductListQuery());
        return Ok(products);
    }
}
