using ECommerce.Application.Features.Payments.DTOs;

public sealed class PaymentProfile : Profile
{
    public PaymentProfile()
    {
        CreateMap<Payment, PaymentDto>();

        CreateMap<Payment, PaymentListItemDto>();
    }
}