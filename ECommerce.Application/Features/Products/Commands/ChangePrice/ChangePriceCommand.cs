using MediatR;

namespace ECommerce.Application.Features.Products.Commands.ChangePrice
{
    // Placeholder command for changing product price
    public sealed record ChangePriceCommand(
        int ProductId,
        decimal Price,
        decimal? DiscountPrice)
        : IRequest<Result>;
}
