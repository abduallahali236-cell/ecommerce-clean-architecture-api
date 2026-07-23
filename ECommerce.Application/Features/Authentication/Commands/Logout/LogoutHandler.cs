
namespace ECommerce.Application.Features.Authentication.Commands.Logout;

public sealed class LogoutHandler
    : IRequestHandler<LogoutCommand, Result>
{
    private readonly IIdentityService _identityService;
    private readonly ICurrentUserService _currentUser;

    public LogoutHandler(
        IIdentityService identityService,
        ICurrentUserService currentUser)
    {
        _identityService = identityService;
        _currentUser = currentUser;
    }

    public async Task<Result> Handle(
        LogoutCommand request,
        CancellationToken cancellationToken)
    {
        return await _identityService.LogoutAsync(
            _currentUser.UserId!.Value,
            cancellationToken);
    }
}