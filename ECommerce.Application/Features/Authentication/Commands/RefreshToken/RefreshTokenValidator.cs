namespace ECommerce.Application.Features.Authentication.Commands.RefreshToken;

public sealed class RefreshTokenValidator
        : AbstractValidator<RefreshTokenCommand>
    {
        public RefreshTokenValidator()
        {
            RuleFor(x => x.RefreshToken)
                .NotEmpty();
        }
    }