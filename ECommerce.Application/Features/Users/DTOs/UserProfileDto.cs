namespace ECommerce.Application.Features.Users.DTOs;

public sealed record UserProfileDto(
    int Id,
    string FullName,
    string Email);