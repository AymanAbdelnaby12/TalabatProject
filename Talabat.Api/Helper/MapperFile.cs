using AutoMapper;
using Microsoft.EntityFrameworkCore.Storage;
using Talabat.Api.Dtos;
using Talabat.Api.Error;
using Talabat.Core.Models;
using Talabat.Core.Models.Identity;

namespace Talabat.Api.Helper
{
    public class MapperFile : Profile
    {
        public MapperFile()
        {
            CreateMap<Product, ProductDto>()
                .ForMember(d=>d.ProductBrandName,o=>o.MapFrom(S=>S.ProductBrand.Name))
                .ForMember(d=>d.ProductTypeName,o=>o.MapFrom(S=>S.ProductType.Name))
                .ForMember(d=>d.PictureUrl , o=>o.MapFrom<ProductPicUrlResolver>());
            CreateMap<Address, AddressDto>();

        }
    }
}
