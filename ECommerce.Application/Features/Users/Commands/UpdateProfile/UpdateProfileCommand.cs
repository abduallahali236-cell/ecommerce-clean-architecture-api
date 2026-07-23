namespace ECommerce.Application.Features.Users.Commands.UpdateProfile;

public sealed record UpdateProfileCommand(
    string FullName)
    : IRequest<Result>;