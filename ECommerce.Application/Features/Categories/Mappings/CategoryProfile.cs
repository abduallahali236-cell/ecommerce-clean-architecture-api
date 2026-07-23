using AutoMapper;
using ECommerce.Domain.Entities;
using ECommerce.Application.Features.Categories.DTOs;

namespace ECommerce.Application.Features.Categories.Mappings
{
    public sealed class CategoryProfile : Profile
    {
        public CategoryProfile()
        {
            CreateMap<Category, CategoryDto>();

            CreateMap<Category, CategoryListItemDto>();
        }
    }
}
