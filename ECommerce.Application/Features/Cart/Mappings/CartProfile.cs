using AutoMapper;
using ECommerce.Domain.Entities;
using ECommerce.Application.Features.Cart.DTOs;

namespace ECommerce.Application.Features.Cart.Mappings
{
    sealed class CartProfile : Profile
    {
        public CartProfile()
        {
            CreateMap<CartItem, CartItemDto>();
            CreateMap<Domain.Entities.Cart, CartDto>();
        }
    }
}
