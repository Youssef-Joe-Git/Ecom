using AutoMapper;
using Ecom.Core.Dtos;
using Ecom.Core.Entities.Product;

namespace Ecom.API.Mapping
{
    public class ImageMapping : Profile
    {
        public ImageMapping()
        {
            CreateMap<Image, ImageDto>().ReverseMap();
        }
    }
}
