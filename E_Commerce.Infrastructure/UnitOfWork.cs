using E_Commerce.Domain.Common;
using E_Commerce.Domain.Contracts;
using E_Commerce.Domain.Contracts.Repositories;
using E_Commerce.Infrastructure.Data;
using E_Commerce.Infrastructure.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce.Infrastructure
{
    public class UnitOfWork(StoreDbContext context) : IUnitOfWork
    {
        private readonly Dictionary<string, object> _repositories = [];
        public IGenaricRepository<TEntity, Tkey> GetRepository<TEntity, Tkey>() where TEntity : BaseEntity<Tkey>
        {
            var typeName = typeof(TEntity).Name;
            if (_repositories.TryGetValue(typeName, out object? value))
                return (IGenaricRepository<TEntity, Tkey>)value;

            var repo = new GenericRepositories<TEntity, Tkey>(context);
            _repositories.Add(typeName, repo);
            return repo;
        }




        public Task<int> SaveChangesAsync(CancellationToken ct = default)
        => context.SaveChangesAsync(ct);
    }
}
