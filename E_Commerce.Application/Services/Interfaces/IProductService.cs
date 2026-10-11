using E_Commerce.Application.Common;
using E_Commerce.Application.DTOS.ProductDtos;
using E_Commerce.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce.Application.Services.Interfaces
{
    public interface IProductService
    {
        Task<Result<PaginationResult<ProductDto>>> GetAllProductsAsync(ProductQueryParams queryParams, CancellationToken ct = default);
        Task<Result<ProductDto>> GetAllProductByIdAsync(int id, CancellationToken ct = default);
        Task<Result<IReadOnlyList<BrandDto>>> GetAllBrandsAsync(CancellationToken ct = default);
        Task<Result<IReadOnlyList<TypeDto>>> GetAllTypesAsync(CancellationToken ct = default);


    }
}
