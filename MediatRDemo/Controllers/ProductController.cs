using MediatRDemo.Commands;
using MediatRDemo.Notifications;
using MediatRDemo.Queries;

namespace MediatRDemo.Controllers;

using MediatRDemo.Commands;
using MediatRDemo.Notifications;
using MediatRDemo.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;

[Route("api/mediator/[controller]")]
[ApiController]
public class ProductController : ControllerBase
{
    private readonly ISender _sender;
    private readonly IPublisher _publisher;
    public ProductController(ISender sender, IPublisher publisher)
    {
        _sender = sender;
        _publisher = publisher;
    }

    [HttpGet]
    public async Task<ActionResult> GetProducts()
    {
        var products = await _sender.Send(new GetProductsQuery());
        return Ok(products);
    }
    [HttpGet("GetProductById/{id:int}")]
    public async Task<ActionResult> GetProductById(int id)
    {
        var product = await _sender.Send(new GetProductById(id));
        return Ok(product);
    }
    [HttpPost("AddProduct")]
    public async Task<ActionResult> AddProduct([FromBody] Product product)
    {
        var result = await _sender.Send(new AddProductCommand(product.Id, product.Name));
        await _publisher.Publish(new ProductAddedNotification(result));
        return CreatedAtAction(nameof(GetProductById), new { id = result.Id }, result);
    }
}