using ECommerce.Application.Features.Users.DTOs;

namespace ECommerce.Application.Features.Users.Queries.GetProfile;

public sealed record GetProfileQuery()
    : IRequest<Result<UserProfileDto>>;