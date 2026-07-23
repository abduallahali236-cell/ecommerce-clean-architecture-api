using ECommerce.Application.Features.Authentication.DTOs;
using System;
using System.Collections.Generic;
using System.Text;

namespace ECommerce.Application.Features.Authentication.Commands.Register
{
    public sealed class RegisterHandler
        : IRequestHandler<RegisterCommand, Result<AuthResultDto>>
    {
        private readonly IIdentityService _identityService;

        public RegisterHandler(IIdentityService identityService)
        {
            _identityService = identityService;
        }

        public async Task<Result<AuthResultDto>> Handle(
            RegisterCommand request,
            CancellationToken cancellationToken)
        {
            return await _identityService.RegisterAsync(
                new RegisterRequest(
                    request.FullName,
                    request.Email,
                    request.Password),
                cancellationToken);
        }
    }
}
