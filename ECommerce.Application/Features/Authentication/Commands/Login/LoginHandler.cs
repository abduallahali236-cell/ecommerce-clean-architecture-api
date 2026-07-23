using ECommerce.Application.Features.Authentication.DTOs;
using System;
using System.Collections.Generic;
using System.Text;

namespace ECommerce.Application.Features.Authentication.Commands.Login
{
    public sealed class LoginHandler
        : IRequestHandler<LoginCommand, Result<AuthResultDto>>
    {
        private readonly IIdentityService _identityService;

        public LoginHandler(IIdentityService identityService)
        {
            _identityService = identityService;
        }

        public async Task<Result<AuthResultDto>> Handle(
            LoginCommand request,
            CancellationToken cancellationToken)
        {
            return await _identityService.LoginAsync(
                new LoginRequest(
                    request.Email,
                    request.Password),
                cancellationToken);
        }
    }
}
