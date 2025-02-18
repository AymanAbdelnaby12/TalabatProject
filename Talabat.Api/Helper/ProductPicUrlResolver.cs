using AutoMapper;
using Talabat.Api.Dtos;
using Talabat.Core.Models;

namespace Talabat.Api.Helper
{
    // This class To ResolVe PictreURL
    public class ProductPicUrlResolver : IValueResolver<Product, ProductDto, string>
    {
        private readonly IConfiguration _configuration;

        public ProductPicUrlResolver(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public string Resolve(Product source, ProductDto destination, string destMember, ResolutionContext context)
        {
            if (!string.IsNullOrEmpty(source.PictureUrl))
            {
                return $"{_configuration["ApiBaseUrl"]}{source.PictureUrl}";
            }
            return string.Empty;
            
        }
    }
}
