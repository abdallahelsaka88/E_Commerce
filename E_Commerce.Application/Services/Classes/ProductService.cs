using AutoMapper;
using E_Commerce.Application.Common;
using E_Commerce.Application.DTOS.ProductDtos;
using E_Commerce.Application.Services.Interfaces;
using E_Commerce.Application.Specification;
using E_Commerce.Domain.Common;
using E_Commerce.Domain.Contracts;
using E_Commerce.Domain.Entities.Products;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce.Application.Services.Classes
{
    public class ProductService(IUnitOfWork unitOfWork, IMapper mapper) : IProductService
    {
        public async Task<Result<IReadOnlyList<BrandDto>>> GetAllBrandsAsync(CancellationToken ct = default)
        {
            var brands = await unitOfWork.GetRepository<ProductBrand, int>().GetAllAsync(ct);

            var brandDtos = mapper.Map<IReadOnlyList<BrandDto>>(brands);
            return Result<IReadOnlyList<BrandDto>>.OK(brandDtos);

        }

        public async Task<Result<PaginationResult<ProductDto>>> GetAllProductsAsync(ProductQueryParams queryParams, CancellationToken ct = default)
        {
            var specs = new ProductsWithBrandsAndTypeSpecifications(queryParams);

            var products = await unitOfWork.GetRepository<Product, int>().GetAllAsync(specs, ct);

            var productDtos = mapper.Map<IReadOnlyList<ProductDto>>(products);

            var countSpecs = new ProductCountSpecification(queryParams);
            var count = await unitOfWork.GetRepository<Product, int>().CountAsync(countSpecs, ct);
            var value = new PaginationResult<ProductDto>
                (
                    queryParams.PageIndex,
                    queryParams.PageSize, count, productDtos
                );
            return Result<PaginationResult<ProductDto>>.OK(value);
        }

        public async Task<Result<IReadOnlyList<TypeDto>>> GetAllTypesAsync(CancellationToken ct = default)
        {
            var types = await unitOfWork.GetRepository<ProductType, int>().GetAllAsync(ct);

            var typeDtos = mapper.Map<IReadOnlyList<TypeDto>>(types);
            return Result<IReadOnlyList<TypeDto>>.OK(typeDtos);
        }

        public async Task<Result<ProductDto>> GetAllProductByIdAsync(int id, CancellationToken ct = default)
        {

            var specs = new ProductsWithBrandsAndTypeSpecifications(id);
            var product = await unitOfWork.GetRepository<Product, int>().GetByIdAsync(specs, id, ct);

            if (product is null)
            {
                return Result<ProductDto>.Fail(Error.NotFound($"Product.NotFound", "The Product With {id} Not Found"));
            }

            var productDto = mapper.Map<ProductDto>(product);
            return Result<ProductDto>.OK(productDto);
        }

    }
}
