using ECommerce.Application.Features.Users.Commands.UpdateProfile;

public sealed class UpdateProfileValidator
    : AbstractValidator<UpdateProfileCommand>
{
    public UpdateProfileValidator()
    {
        RuleFor(x => x.FullName)
            .NotEmpty()
            .MaximumLength(100);
    }
}