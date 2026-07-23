namespace ECommerce.Application.Common.Errors;

public static class AuthenticationErrors
{
    public static readonly Error InvalidCredentials =
        new(
            "Authentication.InvalidCredentials",
            "Invalid email or password.",
            ErrorType.Unauthorized);

    public static readonly Error EmailAlreadyExists =
        new(
            "Authentication.EmailAlreadyExists",
            "Email is already registered.",
            ErrorType.Conflict);

    public static readonly Error InvalidRefreshToken =
        new(
            "Authentication.InvalidRefreshToken",
            "Refresh token is invalid.",
            ErrorType.Unauthorized);

    public static readonly Error UserNotFound =
        new(
            "Authentication.UserNotFound",
            "User was not found.",
            ErrorType.NotFound);

    public static readonly Error PasswordMismatch =
        new(
            "Authentication.PasswordMismatch",
            "Current password is incorrect.",
            ErrorType.Validation);
}