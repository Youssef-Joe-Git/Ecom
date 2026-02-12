using AutoMapper;
using Ecom.Core.Dtos;
using Ecom.Core.Entities.Product;

namespace Ecom.API.Mapping
{
    public class ProductMapping:Profile
    {
        public ProductMapping()
        {
            CreateMap<Product, ProductDto>()
                .ForMember( x => x.CategoryName, op => 
                op.MapFrom(src =>src.Category.Name)).ReverseMap();

            CreateMap<AddProductDto, Product>()
                .ForMember(x => x.Images, op =>
                op.Ignore());
            CreateMap<UpdateProductDto, Product>()
                .ForMember(x => x.Images, op =>
                op.Ignore());

        }
    }
}
