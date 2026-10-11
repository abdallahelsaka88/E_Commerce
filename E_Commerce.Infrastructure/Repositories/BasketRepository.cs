using E_Commerce.Domain.Contracts.Repositories;
using E_Commerce.Domain.Entities.Baskets;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace E_Commerce.Infrastructure.Repositories
{
    public class BasketRepository(IConnectionMultiplexer connection) : IBasketRepository
    {
        private readonly IDatabase _database = connection.GetDatabase(); //In-Memory Database



        public async Task<CustomerBasket?> GetBasketAsync(string basketId, CancellationToken ct)
        {
            var value = await _database.StringGetAsync(basketId);

            if (value.IsNullOrEmpty) return null;
            // Deserialize the JSON string back to a CustomerBasket object
            var basket = JsonSerializer.Deserialize<CustomerBasket>(value!);
            if (basket is null) return null;

            return basket;
        }
        public async Task<CustomerBasket?> CreateOrUpdateBasketAsync(CustomerBasket basket, TimeSpan? timeToLive = default, CancellationToken ct = default)
        {
            // Serialize the basket object to a JSON string
            var value = JsonSerializer.Serialize(basket);

            var result = await _database.StringSetAsync(basket.Id, value, timeToLive ?? TimeSpan.FromDays(7));
            return result ? basket : null;
        }
        public async Task<bool> DeleteBasketAsync(string basketId, CancellationToken ct)
        {
            return await _database.KeyDeleteAsync(basketId);
        }


    }
}
