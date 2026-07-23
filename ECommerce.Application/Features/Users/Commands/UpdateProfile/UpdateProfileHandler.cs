using ECommerce.Application.Features.Users.Commands.UpdateProfile;
using ECommerce.Application.Features.Users.DTOs;

public sealed class UpdateProfileHandler
    : IRequestHandler<UpdateProfileCommand, Result>
{
    private readonly IIdentityService _identityService;
    private readonly ICurrentUserService _currentUser;

    public UpdateProfileHandler(
        IIdentityService identityService,
        ICurrentUserService currentUser)
    {
        _identityService = identityService;
        _currentUser = currentUser;
    }

    public async Task<Result> Handle(
        UpdateProfileCommand request,
        CancellationToken cancellationToken)
    {
        return await _identityService.UpdateProfileAsync(
            _currentUser.UserId!.Value,
            new UpdateProfileRequest(request.FullName),
            cancellationToken);
    }
}