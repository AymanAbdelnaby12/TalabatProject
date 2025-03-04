using AutoMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Talabat.Api.Dtos;
using Talabat.Api.Error;
using Talabat.Api.Helper;
using Talabat.Core.Interfaces_Or_Repository;
using Talabat.Core.Models;
using Talabat.Core.Specifications;
using Talabat.Repository;

namespace Talabat.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
         private readonly IGenericRepository<Product> _ProductRepository;
        private readonly IGenericRepository<ProductType> _productTypeRepository;
        private readonly IGenericRepository<ProductBrand> _productBrandRepository;
        private readonly IMapper _mapper;
        public ProductsController(IGenericRepository<ProductBrand> productBrandRepository,IGenericRepository<Product> productRepository, IGenericRepository<ProductType> productTypeRepository, IMapper mapper)
        {
            _ProductRepository = productRepository;
            _productTypeRepository = productTypeRepository;
            _productBrandRepository = productBrandRepository;
            _mapper = mapper;
        }

        [Authorize]
        // GetAllProducts
        [HttpGet]
        public async Task<ActionResult<Pagination<ProductDto>>> GetProducts([FromQuery] ProductSpecParams Params)
        {
            var spec = new ProductWithBrandAndTypeSpecification(Params);
            var products = await _ProductRepository.GetAllWithSpecAsync(spec);
            var mappedProduct = _mapper.Map<IReadOnlyList< Product>, IReadOnlyList<ProductDto>>(products);

            var countSpec = new ProductWithFilterForCountSpecification(Params);
            var Count = await _ProductRepository.CountAsync(countSpec);
            return Ok(new Pagination<ProductDto>(Params.PageIndex, Params.PageSize, mappedProduct, Count));

        }
        // GetProductsById
        [HttpGet("GetProduct")]
        public async Task<ActionResult<Product>> GetProduct(int id)
        {
            var spec = new ProductWithBrandAndTypeSpecification(id);
            var product = await _ProductRepository.GetEntityWithSpecAsync(spec);
            if (product is null) return NotFound(new ApiResponse(404));
            var mappedProduct = _mapper.Map<Product,ProductDto>(product);
            return Ok(mappedProduct);
        }

        // GetAllProductTypes
        [HttpGet("GetProductTypes")]
        public async Task<ActionResult<IReadOnlyList<ProductType>>> GetProductTypes()
        {
            var productBrands = await _productTypeRepository.GetAllAsync();
            return Ok(productBrands);
        }

        // GetProductTypesById
        [HttpGet("GetProductTypesById")]
        public async Task<ActionResult<ProductType>> GetProductTypesById(int id)
        {
           var ProductType =await _productTypeRepository.GetByIdAsync(id);
            if (ProductType is null) return NotFound(new ApiResponse(404));
            return Ok(ProductType);
        }

        // GetAllProductBrands
        [HttpGet("GetAllProductBrands")]
        public async Task<ActionResult<IReadOnlyList<ProductBrand>>> GetAllProductBrands()
        {
            var ProductBrand = await _productBrandRepository.GetAllAsync();
            return Ok(ProductBrand);
        }

        // GetProductBrandById
        [HttpGet("GetProductBrandById")]
        public async Task<ActionResult<ProductBrand>> GetProductBrandById(int id)
        {
            var ProductBrand = await _productBrandRepository.GetByIdAsync(id);
            if (ProductBrand is null) return NotFound(new ApiResponse(404));
            return Ok(ProductBrand);
        }

    }
}
