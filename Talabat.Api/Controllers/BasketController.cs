using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Talabat.Api.Dtos;
using Talabat.Api.Error;
using Talabat.Core.Interfaces_Or_Repository;
using Talabat.Core.Models;

namespace Talabat.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BasketController : ControllerBase
    {
        private readonly IBasketRepository _basketRepository;
        private readonly IMapper _mapper;

        public BasketController(IBasketRepository basketRepository, IMapper mapper)
        {
            _basketRepository = basketRepository;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<CustomerBasket>> GetBasketById(string BasketId)
        {
            var basket = await _basketRepository.GetBasketByIdAsync(BasketId);
            return basket is null ? new CustomerBasket(BasketId) : basket;
        }
        [HttpPost]
        public async Task<ActionResult<CustomerBasket>> UpdateBasket(CustomerBasketDto basket)
        {
            var mappedBasket = _mapper.Map<CustomerBasket>(basket);
            var CreatedOrUpdatedBasket = await _basketRepository.UpdateBasketAsync(mappedBasket);
            if (CreatedOrUpdatedBasket is null) return BadRequest(new ApiResponse(400));
            return Ok(CreatedOrUpdatedBasket);
        }
        [HttpDelete]
        public async Task DeleteBasket(string BasketId)
        {
            await _basketRepository.DeleteBasketAsync(BasketId);
        }
    }
}
