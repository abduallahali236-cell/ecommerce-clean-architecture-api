using ECommerce.API.Extensions;
using ECommerce.Application.Common.Constants;
using ECommerce.Application.Features.Categories.Commands.CreateCategory;
using ECommerce.Application.Features.Categories.Commands.DeleteCategory;
using ECommerce.Application.Features.Categories.Commands.UpdateCategory;
using ECommerce.Application.Features.Categories.Queries.GetCategories;
using ECommerce.Application.Features.Categories.Queries.GetCategoryById;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ECommerce.API.Controllers;

[Route("api/categories")]
public sealed class CategoriesController : BaseApiController
{
    public CategoriesController(ISender sender)
        : base(sender)
    {
    }

    [HttpGet]
    public async Task<IActionResult> GetAll(
        [FromQuery] GetCategoriesQuery query)
        => (await Sender.Send(query)).ToActionResult(this);

    [HttpGet("{id:int}")]
    public async Task<IActionResult> Get(int id)
        => (await Sender.Send(new GetCategoryByIdQuery(id)))
            .ToActionResult(this);

    [Authorize(Roles = Roles.Admin)]
    [HttpPost]
    public async Task<IActionResult> Create(
        CreateCategoryCommand command)
        => (await Sender.Send(command)).ToActionResult(this);

    [Authorize(Roles = Roles.Admin)]
    [HttpPut("{id:int}")]
    public async Task<IActionResult> Update(
        int id,
        UpdateCategoryCommand command)
    {
        if (id != command.Id)
            return BadRequest();

        return (await Sender.Send(command))
            .ToActionResult(this);
    }

    [Authorize(Roles = Roles.Admin)]
    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Delete(int id)
        => (await Sender.Send(new DeleteCategoryCommand(id)))
            .ToActionResult(this);
}