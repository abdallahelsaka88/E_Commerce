using E_Commerce.Application.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Text;

namespace GymManagementApp.API.Attributes
{
    public class RedisCacheAttribute(int timeInSecond) : ActionFilterAttribute
    {
        public async override Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            //Get Cache From DI
            var cacheService = context.HttpContext.RequestServices.GetRequiredService<ICacheService>();
            //Check if the cached data exists 

            var cacheKey = CreateCacheKey(context.HttpContext.Request);
            var value = await cacheService.GetAsync(cacheKey);

            if (!string.IsNullOrEmpty(value))
            {
                //return the cached data as a response
                context.Result = new ContentResult()
                {
                    Content = value,
                    StatusCode = 200,
                    ContentType = "application/json"
                };
                return;

            }
            else
            {
                //call  the api method and cache the result
                var executed = await next.Invoke();
                if (executed.Result is OkObjectResult okResult)
                {
                    await cacheService.SetAsync(cacheKey, okResult.Value, TimeSpan.FromSeconds(timeInSecond));
                }
            }

            //if exists, return the cached data
            //else => Execute the action and cache the result


            return;
        }

        private static string CreateCacheKey(HttpRequest request)
        {
            var key = new StringBuilder();
            key.Append(request.Path);
            foreach (var item in request.Query)
            {
                key.Append($"{item.Key}| {item.Value}");
            }

            return key.ToString();
        }
    }
}
