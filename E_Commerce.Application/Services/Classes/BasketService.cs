using AutoMapper;
using E_Commerce.Application.Common;
using E_Commerce.Application.DTOS.Baskets;
using E_Commerce.Application.Services.Interfaces;
using E_Commerce.Domain.Contracts.Repositories;
using E_Commerce.Domain.Entities.Baskets;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce.Application.Services.Classes
{
    public class BasketService(IBasketRepository basketRepository, IMapper mapper) : IBasketService
    {
        public async Task<Result<BasketDto>> CreateOrUpdateBasketAsync(BasketDto dto, TimeSpan? timeToLive = null, CancellationToken ct = default)
        {
            var basket = mapper.Map<CustomerBasket>(dto);
            var result = await basketRepository.CreateOrUpdateBasketAsync(basket, timeToLive, ct);
            if (result is null)
                return Result<BasketDto>.Fail(Error.Failure("Basket.CreateOrUpdateBasketAsync.Failure", "Failed To Create Or Update Basket"));

            //else
            return Result<BasketDto>.OK(dto);
        }

        public async Task<Result<bool>> DeleteBasketAsync(string id, CancellationToken ct = default)
        {

            var basket = await basketRepository.GetBasketAsync(id, ct);
            if (basket is null)
                return Result<bool>.Fail(Error.NotFound("Basket.NotFound", $"Basket With Id {id} Is Not Found"));

            var delete = await basketRepository.DeleteBasketAsync(id, ct);
            if (delete)
                return Result<bool>.OK(true);

            return Result<bool>.Fail(Error.NotFound("Basket.DeleteBasketAsync.NotFound", "Basket Not Found To Delete"));

        }

        public async Task<Result<BasketDto>> GetBasketAsync(string id, CancellationToken ct = default)
        {
            var basket = await basketRepository.GetBasketAsync(id, ct);
            if (basket is null)
                return Result<BasketDto>.Fail(Error.NotFound("Basket.NotFound", $"Basket With Id {id} Is Not Found"));

            //else
            var dto = mapper.Map<BasketDto>(basket);
            return Result<BasketDto>.OK(dto);

        }
    }
}
