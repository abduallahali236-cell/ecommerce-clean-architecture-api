using ECommerce.Application.Features.Authentication.DTOs;
using System;
using System.Collections.Generic;
using System.Text;

namespace ECommerce.Application.Features.Authentication.Commands.Register
{
    public sealed record RegisterCommand(
        string FullName,
        string Email,
        string Password)
        : IRequest<Result<AuthResultDto>>;
}
