using E_Commerce.Domain.Contracts;
using E_Commerce.Domain.Entities.Identity;
using E_Commerce.Infrastructure.Identity.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce.Infrastructure.DataSeedingImplementation
{
    internal class IdentityDataSeeder(
        StoreIdentityDbContext context,
        UserManager<ApplicationUser> userManager,
        RoleManager<IdentityRole> roleManager,
        ILogger<IdentityDataSeeder> logger

        ) : IDataSeeder
    {
        public async Task SeedDataAsync(CancellationToken ct = default)
        {
            //Check the database Exists and updated
            try
            {

                var pendingMigration = await context.Database.GetPendingMigrationsAsync();

                if (pendingMigration.Any())
                {
                    //update database
                    await context.Database.MigrateAsync();
                }

                //Seed Data For Role
                if (!await roleManager.Roles.AnyAsync())
                {
                    await roleManager.CreateAsync(new IdentityRole("Admin"));
                    await roleManager.CreateAsync(new IdentityRole("SuperAdmin"));
                }

                //Seed Data For User
                if (!await userManager.Users.AnyAsync())
                {
                    var users = new ApplicationUser()
                    {
                        DisplayName = "admin",
                        UserName = "admin",
                        PhoneNumber = "012565688",
                        Email = "admin@system.com"
                    };
                    var result = await userManager.CreateAsync(users, "p@ssw0rd");
                    if (result.Succeeded)
                    {
                        await userManager.AddToRoleAsync(users, "Admin");
                    }
                    else
                    {
                        var error = string.Join(" , \n", result.Errors.Select(e => e.Description));
                        logger.LogWarning($"Cannot Seed Default Admin{error}");
                    }

                }



            }
            catch (Exception ex)
            {
                logger.LogError(ex.Message);
            }
        }
    }
}
