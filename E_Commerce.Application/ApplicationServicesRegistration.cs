using E_Commerce.Application.Services.Classes;
using E_Commerce.Application.Services.Interfaces;
using E_Commerce.Domain.Contracts.Repositories;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
namespace E_Commerce.Application
{
    public static class ApplicationServicesRegistration
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {
            services.AddAutoMapper(c => { }, typeof(ApplicationServicesRegistration).Assembly);

            services.AddScoped<IProductService, ProductService>();
            services.AddScoped<IBasketService, BasketService>();
            services.AddScoped<ICacheService, CacheService>();
            services.AddScoped<IAuthenticationServices, AuthenticationService>();
            services.AddScoped<IOrderService, OrderService>();
            return services;
        }
    }
}
