using ECommerce.Application.Features.Authentication.Commands.Login;
using ECommerce.Application.Features.Authentication.Commands.Register;
using ECommerce.Application.Features.Authentication.Commands.RefreshToken;
using ECommerce.Application.Features.Authentication.Commands.Logout;
using ECommerce.Application.Features.Authentication.Commands.ChangePassword;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MediatR;
using ECommerce.API.Extensions;

namespace ECommerce.API.Controllers;

public sealed class AuthController : BaseApiController
{
    public AuthController(ISender sender) : base(sender)
    {
    }

    [HttpPost("register")]
    public async Task<IActionResult> Register(RegisterCommand command)
    {
        var result = await Sender.Send(command);

        if (!result.IsSuccess)
            return result.ToActionResult(this);

        return CreatedAtAction(
            nameof(UsersController.Profile),
            "Users",
            null,
            result.Value);
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login(LoginCommand command)
    {
        var result = await Sender.Send(command);

        return result.IsSuccess
            ? Ok(result.Value)
            : Unauthorized(result.Errors);
    }

    [HttpPost("refresh")]
    public async Task<IActionResult> Refresh(
        RefreshTokenCommand command)
    {
        var result = await Sender.Send(command);

        return result.IsSuccess
            ? Ok(result.Value)
            : Unauthorized(result.Errors);
    }

    [Authorize]
    [HttpPost("logout")]
    public async Task<IActionResult> Logout()
    {
        var result = await Sender.Send(new LogoutCommand());

        return result.IsSuccess
            ? NoContent()
            : BadRequest(result.Errors);
    }

    [Authorize]
    [HttpPut("change-password")]
    public async Task<IActionResult> ChangePassword(
        ChangePasswordCommand command)
    {
        var result = await Sender.Send(command);

        return result.IsSuccess
            ? NoContent()
            : BadRequest(result.Errors);
    }
}