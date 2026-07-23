using ECommerce.Application.Features.Authentication.DTOs;

namespace ECommerce.Application.Features.Authentication.Commands.RefreshToken;

public sealed class RefreshTokenHandler
    : IRequestHandler<
        RefreshTokenCommand,
        Result<AuthResultDto>>
{
    private readonly IIdentityService _identityService;

    public RefreshTokenHandler(
        IIdentityService identityService)
    {
        _identityService = identityService;
    }

    public async Task<Result<AuthResultDto>> Handle(
        RefreshTokenCommand request,
        CancellationToken cancellationToken)
    {
        return await _identityService
            .RefreshTokenAsync(
                request.RefreshToken,
                cancellationToken);
    }
}