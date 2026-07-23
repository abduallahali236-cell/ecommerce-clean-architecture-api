namespace ECommerce.Application.Features.Authentication.DTOs;

public sealed record RegisterRequest(
    string FullName,
    string Email,
    string Password);