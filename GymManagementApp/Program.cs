using E_Commerce.Application;
using E_Commerce.Domain.Contracts;
using E_Commerce.Domain.Entities.Identity;
using E_Commerce.Infrastructure;
using E_Commerce.Infrastructure.Identity.Data;
using GymManagementApp.API.Extentions;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.FileProviders;
using System.Threading.Tasks;
namespace GymManagementApp
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
            builder.Services.AddOpenApi();

            builder.Services.AddInfrastructureServices(builder.Configuration);
            builder.Services.AddApplicationServices();
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();



            var app = builder.Build();
            await app.SeedMigrationDataAsync();
            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
                app.MapOpenApi();
            }

            app.UseStaticFiles(new StaticFileOptions()
            {
                FileProvider = new PhysicalFileProvider(Path.Combine(builder.Environment.ContentRootPath, "Files"))
            });

            app.UseHttpsRedirection();
            app.UseAuthentication();
            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
