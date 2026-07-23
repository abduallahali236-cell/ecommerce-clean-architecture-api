using ECommerce.API.Extensions;
using ECommerce.Application.Common.Constants;
using ECommerce.Application.Features.Payments.Commands.ConfirmPayment;
using ECommerce.Application.Features.Payments.Commands.CreatePayment;
using ECommerce.Application.Features.Payments.Commands.FailPayment;
using ECommerce.Application.Features.Payments.Commands.RefundPayment;
using ECommerce.Application.Features.Payments.Queries.GetPayment;
using ECommerce.Application.Features.Payments.Queries.GetPayments;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ECommerce.API.Controllers;

[Authorize]
[Route("api/payments")]
public sealed class PaymentsController : BaseApiController
{
    public PaymentsController(ISender sender)
        : base(sender)
    {
    }

    [HttpGet]
    public async Task<IActionResult> GetAll(
        [FromQuery] GetPaymentsQuery query)
        => (await Sender.Send(query))
            .ToActionResult(this);

    [HttpGet("{id:int}")]
    public async Task<IActionResult> Get(
        int id)
        => (await Sender.Send(new GetPaymentQuery(id)))
            .ToActionResult(this);

    [HttpPost]
    public async Task<IActionResult> Create(
        CreatePaymentCommand command)
        => (await Sender.Send(command))
            .ToActionResult(this);

    [Authorize(Roles = Roles.Admin)]
    [HttpPost("{id:int}/confirm")]
    public async Task<IActionResult> Confirm(
        int id)
        => (await Sender.Send(
                new ConfirmPaymentCommand(id)))
            .ToActionResult(this);

    [Authorize(Roles = Roles.Admin)]
    [HttpPost("{id:int}/fail")]
    public async Task<IActionResult> Fail(
        int id)
        => (await Sender.Send(
                new FailPaymentCommand(id)))
            .ToActionResult(this);

    [Authorize(Roles = Roles.Admin)]
    [HttpPost("{id:int}/refund")]
    public async Task<IActionResult> Refund(
        int id)
        => (await Sender.Send(
                new RefundPaymentCommand(id)))
            .ToActionResult(this);
}