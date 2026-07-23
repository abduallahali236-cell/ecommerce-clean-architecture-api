using MediatR;

namespace ECommerce.Application.Features.Categories.Commands.UpdateCategory
{
    public sealed record UpdateCategoryCommand(
        int Id,
        string Name,
        string? Description)
        : IRequest<Result>;
}
