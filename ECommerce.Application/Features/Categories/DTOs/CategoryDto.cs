using System;

namespace ECommerce.Application.Features.Categories.DTOs
{
    public sealed record CategoryDto(
        int Id,
        string Name,
        string? Description);
}
