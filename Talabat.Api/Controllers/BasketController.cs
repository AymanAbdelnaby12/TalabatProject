using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
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

        public BasketController(IBasketRepository basketRepository)
        {
            _basketRepository = basketRepository;
        }

        [HttpGet]
        public async Task<ActionResult<CustomerBasket>> GetBasketById(string BasketId)
        {
            var basket = await _basketRepository.GetBasketByIdAsync(BasketId);
            return basket is null ? new CustomerBasket(BasketId) : basket;
        }
        [HttpPost]
        public async Task<ActionResult<CustomerBasket>> UpdateBasket(CustomerBasket basket)
        {
            var CreatedOrUpdatedBasket = await _basketRepository.UpdateBasketAsync(basket);
            if (CreatedOrUpdatedBasket is null) return BadRequest(new ApiResponce(400));
            return Ok(CreatedOrUpdatedBasket);
        }
        [HttpDelete]
        public async Task DeleteBasket(string BasketId)
        {
            await _basketRepository.DeleteBasketAsync(BasketId);
        }
    }
}
