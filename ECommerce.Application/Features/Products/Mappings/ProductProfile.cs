using System;
using System.Collections.Generic;
using System.Text;
using AutoMapper;
using ECommerce.Domain.Entities;
using ECommerce.Application.Features.Products.DTOs;

namespace ECommerce.Application.Features.Products.Mappings
{
    public sealed class ProductProfile : Profile
    {
        public ProductProfile()
        {
            CreateMap<Product, ProductDto>()
                .ForCtorParam(
                    "Category",
                    opt => opt.MapFrom(src => src.Category.Name));

            CreateMap<Product, ProductListItemDto>()
                .ForCtorParam(
                    "Category",
                    opt => opt.MapFrom(src => src.Category.Name));
        }
    }
}
