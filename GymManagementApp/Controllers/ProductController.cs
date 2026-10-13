using E_Commerce.Application.Common;
using E_Commerce.Application.DTOS.ProductDtos;
using E_Commerce.Application.Services.Interfaces;
using E_Commerce.Domain.Common;
using GymManagementApp.API.Attributes;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace GymManagementApp.API.Controllers
{

    public class ProductController(IProductService productService) : ApiBaseController
    {

        //GET: api/Products
        [HttpGet]
        [RedisCache(60)]
        [Authorize]
        public async Task<ActionResult<PaginationResult<ProductDto>>> GetAllProductAsync([FromQuery] ProductQueryParams queryParams, CancellationToken ct)
        {
            var result = await productService.GetAllProductsAsync(queryParams, ct);

            return ToActionResult(result);
        }

        //GET: api/Products
        [HttpGet("brands")]
        public async Task<ActionResult<IReadOnlyList<BrandDto>>> GetAllBrandAsync(CancellationToken ct)
        {
            var result = await productService.GetAllBrandsAsync(ct);

            return ToActionResult(result);
        }

        [HttpGet("types")]
        public async Task<ActionResult<IReadOnlyList<TypeDto>>> GetAllTypeAsync(CancellationToken ct)
        {
            var result = await productService.GetAllTypesAsync(ct);

            return ToActionResult(result);
        }



        [HttpGet("{id}")]
        public async Task<ActionResult<ProductDto>> GetProductByIdAsync(int id, CancellationToken ct)
        {
            var result = await productService.GetAllProductByIdAsync(id, ct);

            return ToActionResult(result);
        }


    }
}
