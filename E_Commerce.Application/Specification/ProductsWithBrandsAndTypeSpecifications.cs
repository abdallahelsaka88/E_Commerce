using E_Commerce.Domain.Common;
using E_Commerce.Domain.Entities.Products;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce.Application.Specification
{
    public class ProductsWithBrandsAndTypeSpecifications : BaseSpecification<Product, int>
    {
        public ProductsWithBrandsAndTypeSpecifications(ProductQueryParams queryParams) :

        base
        (
            P =>
            (!queryParams.BrandId.HasValue || P.BrandId == queryParams.BrandId)
            &&
            (!queryParams.TypeId.HasValue || P.TypeId == queryParams.TypeId)
            &&
            (string.IsNullOrWhiteSpace(queryParams.SearchValue) || P.Name.ToLower().Contains(queryParams.SearchValue.ToLower()))

        )



        {
            AddInclude(p => p.Brand);
            AddInclude(p => p.Type);


            switch (queryParams.Sort)
            {
                case ProductSortOptions.NameDesc:
                    AddOrderByDesc(p => p.Name);
                    break;

                case ProductSortOptions.NameAsc:
                    AddOrderBy(p => p.Name);
                    break;

                case ProductSortOptions.PriceAsc:
                    AddOrderBy(p => p.Name);
                    break;

                case ProductSortOptions.PriceDesc:
                    AddOrderByDesc(p => p.Name);
                    break;
                default:
                    AddOrderBy(p => p.Name);
                    break;
            }


            ApplyPagination(queryParams.PageIndex, queryParams.PageSize);


        }
        public ProductsWithBrandsAndTypeSpecifications(int id) : base(p => p.Id == id)
        {
            AddInclude(p => p.Brand);
            AddInclude(p => p.Type);
        }
    }
}
