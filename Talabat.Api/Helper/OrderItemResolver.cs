using AutoMapper;
using AutoMapper.Execution;
using Talabat.Api.Dtos;
using Talabat.Core.Models.Order_Aggregate;

namespace Talabat.Api.Helper
{
    public class OrderItemResolver : IValueResolver<OrderItem, OrderItemDto, string>
    {
        private readonly IConfiguration _config;

        public OrderItemResolver(IConfiguration config)
        {
            _config = config;
        }

        public string Resolve(OrderItem source, OrderItemDto destination, string destMember, ResolutionContext context)
        {
            if(!string.IsNullOrEmpty(source.ProductItemOrdered.PictureUrl))
            {
                return $"{_config["ApiBaseUrl"]}{source.ProductItemOrdered.PictureUrl}";
            }
            return string.Empty;
        }
    }
}
