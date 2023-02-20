using Basket.API.Entities;
using Basket.API.Repositories;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Basket.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BasketController : ControllerBase
    {
        private readonly IBasketRepository _basketRepository;
        public BasketController(IBasketRepository basketRepository) 
        {
            _basketRepository = basketRepository ?? throw new ArgumentNullException(nameof(basketRepository));
        }
        // GET: api/<BasketController>
        [HttpGet]
        public async Task<ActionResult<ShoppingCart?>> GetBasket(string userName)
        {
            var basket = await _basketRepository.GetBasket(userName);
            return Ok(basket ?? new ShoppingCart(userName));
        }

        // POST api/<BasketController>
        [HttpPost]
        public async Task<ActionResult<ShoppingCart>> Post([FromBody] ShoppingCart basket)
        {
            return Ok(await _basketRepository.UpdateBasket(basket));
        }

        // DELETE api/<BasketController>/5
        [HttpDelete("{userName}")]
        public async Task<IActionResult> Delete(string userName)
        {
            await _basketRepository.DeleteBasket(userName);
            return Ok();
        }
    }
}
