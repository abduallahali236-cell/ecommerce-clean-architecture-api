using ECommerce.API.Extensions;
using ECommerce.Application.Features.Cart.Commands.AddToCart;
using ECommerce.Application.Features.Cart.Commands.ClearCart;
using ECommerce.Application.Features.Cart.Commands.RemoveFromCart;
using ECommerce.Application.Features.Cart.Commands.UpdateCartItem;
using ECommerce.Application.Features.Cart.Commands.UpdateCartItemQuantity;
using ECommerce.Application.Features.Cart.Queries.GetCart;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ECommerce.API.Controllers;

[Authorize]
[Route("api/cart")]
public sealed class CartController : BaseApiController
{
    public CartController(ISender sender)
        : base(sender)
    {
    }

    [HttpGet]
    public async Task<IActionResult> Get()
        => (await Sender.Send(new GetCartQuery()))
            .ToActionResult(this);

    [HttpPost]
    public async Task<IActionResult> Add(
        AddToCartCommand command)
        => (await Sender.Send(command))
            .ToActionResult(this);

    [HttpPut]
    public async Task<IActionResult> Update(
        UpdateCartItemQuantityCommand command)
        => (await Sender.Send(command))
            .ToActionResult(this);

    [HttpDelete("{productId:int}")]
    public async Task<IActionResult> Remove(
        int productId)
        => (await Sender.Send(
                new RemoveFromCartCommand(productId)))
            .ToActionResult(this);

    [HttpDelete]
    public async Task<IActionResult> Clear()
        => (await Sender.Send(new ClearCartCommand()))
            .ToActionResult(this);
}