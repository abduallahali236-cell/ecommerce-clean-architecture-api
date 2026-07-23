using MediatR;

namespace ECommerce.Application.Features.Orders.Commands.PlaceOrder
{
    public sealed record PlaceOrderCommand(
        string FullName,
        string PhoneNumber,
        string City,
        string AddressLine)
        : IRequest<Result<int>>;
}
