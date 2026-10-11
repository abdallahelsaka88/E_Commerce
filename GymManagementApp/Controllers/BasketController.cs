using E_Commerce.Application.DTOS.Baskets;
using E_Commerce.Application.Services.Interfaces;
using GymManagementApp.API.Attributes;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace GymManagementApp.API.Controllers
{
    public class BasketController(IBasketService basketService) : ApiBaseController
    {
        // GET : /api/Basket/{id} #Get basket
        [HttpGet("{id}")]
        [RedisCache(60)]
        public async Task<ActionResult<BasketDto>> GetBasket(string id, CancellationToken ct = default!)
        {
            var result = await basketService.GetBasketAsync(id, ct);
            return ToActionResult(result);
        }


        //POST : /api/Basket # Create Or Update basket
        [HttpPost]
        public async Task<ActionResult<BasketDto>> CreateOrUpdateBasket(BasketDto dto, CancellationToken ct = default!)
        {
            var result = await basketService.CreateOrUpdateBasketAsync(dto, null, ct);
            return ToActionResult(result);
        }

        //DELETE: /api/Basket/{id} # Delete basket
        [HttpDelete("{id}")]
        public async Task<ActionResult<bool>> DeleteBasket(string id, CancellationToken ct = default!)
        {
            var result = await basketService.DeleteBasketAsync(id, ct);
            return ToActionResult(result);
        }


    }
}
