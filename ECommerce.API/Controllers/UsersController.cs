using ECommerce.API.Extensions;
using ECommerce.Application.Features.Users.Commands.UpdateProfile;
using ECommerce.Application.Features.Users.Queries.GetProfile;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ECommerce.API.Controllers;

[Authorize]
[Route("api/users")]
public sealed class UsersController : BaseApiController
{
    public UsersController(ISender sender)
        : base(sender)
    {
    }

    [HttpGet("me")]
    public async Task<IActionResult> Profile()
        => (await Sender.Send(new GetProfileQuery()))
            .ToActionResult(this);

    [HttpPut("me")]
    public async Task<IActionResult> Update(
        UpdateProfileCommand command)
        => (await Sender.Send(command))
            .ToActionResult(this);
}