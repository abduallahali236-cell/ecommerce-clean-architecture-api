using ECommerce.API.Controllers;
using ECommerce.Application.Common.Constants;
using ECommerce.Application.Features.Products.Commands.CreateProduct;
using ECommerce.Application.Features.Products.Commands.DeleteProduct;
using ECommerce.Application.Features.Products.Commands.UpdateProduct;
using ECommerce.Application.Features.Products.Queries.GetProducts;
using ECommerce.Application.Features.Products.Queries.GetProductById;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MediatR;
using ECommerce.API.Extensions;

[Route("api/products")]
public sealed class ProductsController : BaseApiController
{
    public ProductsController(ISender sender) : base(sender)
    {
    }

    [HttpGet]
    public async Task<IActionResult> GetAll(
        [FromQuery] GetProductsQuery query)
    {
        var result = await Sender.Send(query);

        return result.ToActionResult(this);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> Get(int id)
    {
        var result =
            await Sender.Send(new GetProductByIdQuery(id));

        return result.ToActionResult(this);
    }

    [Authorize(Roles = Roles.Admin)]
    [HttpPost]
    public async Task<IActionResult> Create(
    CreateProductCommand command)
    {
        var result = await Sender.Send(command);

        return result.ToActionResult(this);
    }

    [Authorize(Roles = Roles.Admin)]
    [HttpPut("{id:int}")]
    public async Task<IActionResult> Update(
        int id,
        UpdateProductCommand command)
    {
        if (id != command.Id)
            return BadRequest();

        var result = await Sender.Send(command);

        return result.ToActionResult(this);
    }

    [Authorize(Roles = Roles.Admin)]
    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Delete(
        int id)
    {
        var result = await Sender.Send(
            new DeleteProductCommand(id));

        return result.ToActionResult(this);
    }
}