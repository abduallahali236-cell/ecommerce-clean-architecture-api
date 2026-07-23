using AutoMapper;
using ECommerce.Domain.Entities;
using ECommerce.Application.Features.Orders.DTOs;

namespace ECommerce.Application.Features.Orders.Mappings
{
    public sealed class OrderProfile : Profile
    {
        public OrderProfile()
        {
            CreateMap<OrderItem, OrderItemDto>();

            CreateMap<Order, OrderDto>();

            CreateMap<Order, OrderListItemDto>();
        }
    }
}
