using ECommerce.Application.Features.Orders.Commands.CancelOrder;
using ECommerce.Application.Features.Orders.Commands.PlaceOrder;
using ECommerce.Application.Features.Orders.Commands.DeleteOrder;
using ECommerce.Application.Features.Orders.Queries.GetOrderById;
using ECommerce.Application.Features.Orders.Queries.GetMyOrders;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ECommerce.API.Extensions;

namespace ECommerce.API.Controllers;

[Authorize]
[Route("api/orders")]
public sealed class OrdersController : BaseApiController
{
    public OrdersController(ISender sender)
        : base(sender)
    {
    }

    [HttpGet]
    public async Task<IActionResult> GetAll(
        [FromQuery] GetMyOrdersQuery query)
        => (await Sender.Send(query))
            .ToActionResult(this);

    [HttpGet("{id:int}")]
    public async Task<IActionResult> Get(
        int id)
        => (await Sender.Send(new GetOrderByIdQuery(id)))
            .ToActionResult(this);

    [HttpPost]
    public async Task<IActionResult> Create(
        PlaceOrderCommand command)
        => (await Sender.Send(command))
            .ToActionResult(this);

    [HttpPost("{id:int}/cancel")]
    public async Task<IActionResult> Cancel(
        int id)
        => (await Sender.Send(
                new CancelOrderCommand(id)))
            .ToActionResult(this);
}