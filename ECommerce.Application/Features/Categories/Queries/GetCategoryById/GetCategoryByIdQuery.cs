using MediatR;
using ECommerce.Application.Features.Categories.DTOs;

namespace ECommerce.Application.Features.Categories.Queries.GetCategoryById
{
    public sealed record GetCategoryByIdQuery(int Id)
        : IRequest<Result<CategoryDto>>;
}
