using ECommerce.Application.Features.Users.DTOs;
using ECommerce.Application.Features.Users.Queries.GetProfile;

public sealed class GetProfileHandler
    : IRequestHandler<GetProfileQuery, Result<UserProfileDto>>
{
    private readonly IIdentityService _identityService;
    private readonly ICurrentUserService _currentUser;

    public GetProfileHandler(
        IIdentityService identityService,
        ICurrentUserService currentUser)
    {
        _identityService = identityService;
        _currentUser = currentUser;
    }

    public async Task<Result<UserProfileDto>> Handle(
        GetProfileQuery request,
        CancellationToken cancellationToken)
    {
        return await _identityService.GetProfileAsync(
            _currentUser.UserId!.Value,
            cancellationToken);
    }
}