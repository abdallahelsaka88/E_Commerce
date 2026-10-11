using E_Commerce.Application.Services.Interfaces;
using E_Commerce.Domain.Contracts.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce.Application.Services.Classes
{
    internal class CacheService(ICacheRepository cacheRepository) : ICacheService
    {
        public async Task<string?> GetAsync(string key, CancellationToken ct = default)
        {
            return await cacheRepository.GetAsync(key);

        }

        public Task SetAsync(string key, object value, TimeSpan? duration = null, CancellationToken ct = default)
        {
            var result = cacheRepository.SetAsync(key, value, duration ?? TimeSpan.FromMinutes(1), ct);
            return result;
        }
    }
}
