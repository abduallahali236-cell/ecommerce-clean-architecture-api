using ECommerce.Application.Features.Authentication.DTOs;
using System;
using System.Collections.Generic;
using System.Text;

namespace ECommerce.Application.Features.Authentication.Commands.Login
{
    public sealed record LoginCommand(
        string Email,
        string Password)
        : IRequest<Result<AuthResultDto>>;
}
