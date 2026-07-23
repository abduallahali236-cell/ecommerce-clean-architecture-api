namespace ECommerce.Application.Features.Authentication.DTOs;

public sealed record ChangePasswordRequest(
    string CurrentPassword,
    string NewPassword);