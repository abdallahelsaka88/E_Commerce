using E_Commerce.Domain.Common;
using E_Commerce.Domain.Contracts;
using E_Commerce.Domain.Entities.Orders;
using E_Commerce.Domain.Entities.Products;
using E_Commerce.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace E_Commerce.Infrastructure.DataSeedingImplementation
{
    public class CatalogDataSeeder(StoreDbContext context, ILogger<CatalogDataSeeder> logger) : IDataSeeder
    {
        //Check if there are any pending migrations on The Database and apply them if necessary
        public async Task SeedDataAsync(CancellationToken ct = default)
        {
            try
            {
                var pendingMigration = await context.Database.GetPendingMigrationsAsync();

                if (pendingMigration.Any())
                {
                    await context.Database.MigrateAsync();
                }

                //Seeding the data from the JSON files into the database
                var seedrootPath = Path.Combine(AppContext.BaseDirectory, "DataSeed");
                //Brands
                await SeedIfEmpty<ProductBrand, int>(seedrootPath, "brands.json", ct);
                //Types
                await SeedIfEmpty<ProductType, int>(seedrootPath, "types.json", ct);
                //Products
                await SeedIfEmpty<Product, int>(seedrootPath, "products.json", ct);
                //delivery method
                await SeedIfEmpty<DeliveryMethod, int>(seedrootPath, "delivery.json", ct);

                var count = await context.SaveChangesAsync(ct);
                if (count > 0)
                {
                    logger.LogInformation($"{count} Rows Added");
                }
                else
                {
                    logger.LogInformation("Database Already Added");
                }
            }
            catch (Exception ex)
            {
                logger.LogError(ex.Message);
            }
        }




        private async Task SeedIfEmpty<TEntity, TKey>(string rootPath, string fileName, CancellationToken ct = default) where TEntity : BaseEntity<TKey>
        {
            if (await context.Set<TEntity>().AnyAsync())
            {
                var tableName = typeof(TEntity).Name;
                logger.LogInformation($"Table {tableName} Already Has Data");
                return;
            }

            var filePath = Path.Combine(rootPath, fileName);
            if (!File.Exists(filePath))
            {
                logger.LogWarning($"File Path : {filePath} does not exist.");
                return;
            }


            //Seeding
            using var fileStream = File.OpenRead(filePath);
            var options = new JsonSerializerOptions()
            {
                PropertyNameCaseInsensitive = true
            };

            var data = await JsonSerializer.DeserializeAsync<List<TEntity>>(fileStream, options, ct);

            if (data is not null && data.Any())
                await context.Set<TEntity>().AddRangeAsync(data);


        }
    }
}
