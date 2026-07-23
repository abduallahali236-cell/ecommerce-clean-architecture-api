using MediatR;
using System.Collections.Generic;
using ECommerce.Application.Features.Categories.DTOs;

namespace ECommerce.Application.Features.Categories.Queries.GetCategories
{
    public sealed record GetCategoriesQuery(
        int PageNumber = 1,
        int PageSize = 10)
        : PaginationRequest(PageNumber, PageSize),
          IRequest<Result<PaginatedResult<CategoryListItemDto>>>;
}
