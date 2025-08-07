using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.Domain.Abstractions.Repositories
{
    public interface IRepository<T> where T : Entity
    {

        Task<T?> GetByIdAsync(Guid id,Func<IQueryable<T>, IQueryable<T>>? include = null,CancellationToken cancellationToken = default);
        Task<IReadOnlyList<T>> GetAllAsync(Func<IQueryable<T>, IQueryable<T>> include,CancellationToken cancellationToken = default);
        Task AddAsync(T entity, CancellationToken cancellationToken = default);
        void Update(T entity);
        void Remove(T entity);
        Task<bool> SaveChangesAsync(CancellationToken cancellationToken = default);

    }
}
