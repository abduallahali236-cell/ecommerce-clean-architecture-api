namespace ECommerce.Application.Features.Products.DTOs
{
public sealed record ProductFilterDto(
    int? CategoryId,
    decimal? MinPrice,
    decimal? MaxPrice,
    int PageNumber = 1,
    int PageSize = 10);
}
