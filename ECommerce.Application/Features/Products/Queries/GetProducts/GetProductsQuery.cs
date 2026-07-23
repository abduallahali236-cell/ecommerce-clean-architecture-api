using MediatR;
using System.Collections.Generic;
using ECommerce.Application.Common.Models;
using ECommerce.Domain.Entities;
using ECommerce.Application.Features.Products.DTOs;

namespace ECommerce.Application.Features.Products.Queries.GetProducts
{
    public sealed record GetProductsQuery(
        string? Search,
        int? CategoryId,
        decimal? MinPrice,
        decimal? MaxPrice,
        int PageNumber = 1,
        int PageSize = 10)
        : IRequest<Result<PaginatedResult<ProductListItemDto>>>;
}
