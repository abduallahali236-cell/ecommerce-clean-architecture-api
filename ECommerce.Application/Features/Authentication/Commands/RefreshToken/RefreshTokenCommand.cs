using ECommerce.Application.Features.Authentication.DTOs;
namespace ECommerce.Application.Features.Authentication.Commands.RefreshToken;

public sealed record RefreshTokenCommand(
        string RefreshToken)
        : IRequest<Result<AuthResultDto>>;

