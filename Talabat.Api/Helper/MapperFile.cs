using AutoMapper;
using Microsoft.EntityFrameworkCore.Storage;
using Talabat.Api.Dtos;
using Talabat.Api.Error;
using Talabat.Core.Models;
using Talabat.Core.Models.Identity;
using Talabat.Core.Models.Order_Aggregate;

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
            CreateMap<Core.Models.Identity.Address, AddressDto>();

            CreateMap<CustomerBasketDto, CustomerBasket>();
            CreateMap<BasketItemDto, BasketItem>();
           
            CreateMap<AddressDto, Core.Models.Order_Aggregate.Address>();
            CreateMap<Order, OrderToReturnDto>()
               .ForMember(d => d.DeliveryMethod, o => o.MapFrom(s => s.DeliveryMethod.ShortName))
               .ForMember(d => d.DeliveryMethod, o => o.MapFrom(s => s.DeliveryMethod.Cost));

            CreateMap<OrderItem, OrderItemDto>()
                .ForMember(d=>d.ProductId ,o=>o.MapFrom(s=>s.ProductItemOrdered.ProductId))
                .ForMember(d => d.ProductName, o => o.MapFrom(s => s.ProductItemOrdered.ProductName))
                .ForMember(d=>d.PictureUrl ,o=>o.MapFrom(s=>s.ProductItemOrdered.PictureUrl))
                .ForMember(d=>d.PictureUrl ,o=>o.MapFrom<OrderItemResolver>());
        }
    }
}
