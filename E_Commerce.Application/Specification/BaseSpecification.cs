using E_Commerce.Domain.Common;
using E_Commerce.Domain.Specifications;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce.Application.Specification
{
    public class BaseSpecification<TEntity, TKey> : ISpecifications<TEntity, TKey> where TEntity : BaseEntity<TKey>
    {
        public ICollection<Expression<Func<TEntity, object>>> IncludeExpression { get; set; } = [];

        public Expression<Func<TEntity, bool>> Criteria { get; private set; }





        public Expression<Func<TEntity, object>>? OrderBy { get; private set; }

        public Expression<Func<TEntity, object>>? OrderByDesc { get; private set; }




        public int Take { get; private set; }

        public int Skip { get; private set; }

        public bool IsPaginated { get; private set; }


        protected void ApplyPagination(int pageIndex, int pageSize)
        {
            Take = pageSize;
            Skip = (pageIndex - 1) * pageSize;
            IsPaginated = true;
        }

        public BaseSpecification(Expression<Func<TEntity, bool>> expression)
        {
            Criteria = expression;
        }



        protected void AddOrderBy(Expression<Func<TEntity, object>> expression)
        {
            OrderBy = expression;
        }
        protected void AddOrderByDesc(Expression<Func<TEntity, object>> expression)
        {
            OrderByDesc = expression;
        }

        public void AddInclude(Expression<Func<TEntity, object>> expression)
        {
            IncludeExpression.Add(expression);
        }


    }
}
