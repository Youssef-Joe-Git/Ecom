using AutoMapper;
using Ecom.Core.Dto;
using Ecom.Core.Entities.Product;

namespace Ecom.API.Mapping
{
    public class CategoryMapping:Profile
    {
        public CategoryMapping()
        {
            CreateMap<Category, CategoryDto>().ReverseMap();
            CreateMap<Category, UpdateCategoryDto>().ReverseMap();
        }
    }
}
