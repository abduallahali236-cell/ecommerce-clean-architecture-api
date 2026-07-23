using ECommerce.Application.Features.Authentication.Commands.ChangePassword;
using ECommerce.Application.Features.Authentication.DTOs;

namespace ECommerce.Application.Features.Authentication.Commands.ChangePassword;

public sealed class ChangePasswordHandler
    : IRequestHandler<ChangePasswordCommand, Result>
{
    private readonly IIdentityService _identityService;
    private readonly ICurrentUserService _currentUser;

    public ChangePasswordHandler(
        IIdentityService identityService,
        ICurrentUserService currentUser)
    {
        _identityService = identityService;
        _currentUser = currentUser;
    }

    public async Task<Result> Handle(
        ChangePasswordCommand request,
        CancellationToken cancellationToken)
    {
        return await _identityService.ChangePasswordAsync(
            _currentUser.UserId!.Value,
            new ChangePasswordRequest(
                request.CurrentPassword,
                request.NewPassword),
            cancellationToken);
    }
}