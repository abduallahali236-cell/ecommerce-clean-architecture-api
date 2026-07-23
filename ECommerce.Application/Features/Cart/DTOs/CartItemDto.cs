namespace ECommerce.Application.Features.Cart.DTOs
{
    public sealed record CartItemDto(
        int ProductId,
        string ProductName,
        decimal UnitPrice,
        int Quantity,
        decimal TotalPrice,
        string? ImageUrl);
}
