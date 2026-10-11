using E_Commerce.Application.Common;
using E_Commerce.Application.DTOS.Baskets;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce.Application.Services.Interfaces
{
    public interface IBasketService
    {
        //Get Basket By Id => basketId => BasketDto
        Task<Result<BasketDto>> GetBasketAsync(string id, CancellationToken ct = default!);

        //Create Or Update Basket=> BasketDto => BasketDto (After Creation Or Update)
        Task<Result<BasketDto>> CreateOrUpdateBasketAsync(BasketDto dto, TimeSpan? timeToLive = default, CancellationToken ct = default!);

        //Delete Basket By Id=>basketId => bool (True Or False)
        Task<Result<bool>> DeleteBasketAsync(string id, CancellationToken ct = default!);

    }
}
