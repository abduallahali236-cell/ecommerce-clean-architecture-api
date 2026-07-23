namespace ECommerce.Application.Features.Authentication.Commands.Logout;

public sealed record LogoutCommand()
    : IRequest<Result>;