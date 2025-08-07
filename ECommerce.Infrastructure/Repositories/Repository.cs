using ECommerce.Domain.Abstractions;
using ECommerce.Domain.Abstractions.Repositories;
using ECommerce.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace ECommerce.Infrastructure.Repositories
{
    public class Repository<T> : IRepository<T> where T : Entity
    {
        private readonly AppDbContext _context;
        private readonly DbSet<T> _entities;

        public Repository(AppDbContext context)
        {
            _entities = context.Set<T>();
            _context = context;
        }

        public async Task AddAsync(T entity, CancellationToken cancellationToken = default)
        {
            await _entities.AddAsync(entity, cancellationToken);
        }

        public async Task<IReadOnlyList<T>> GetAllAsync(Func<IQueryable<T>, IQueryable<T>> include,CancellationToken cancellationToken = default)
        {
            IQueryable<T> query = _entities.AsQueryable();

            if (include != null)
                query = include(query);

            return await query.ToListAsync(cancellationToken);
        }

        public async Task<T?> GetByIdAsync(Guid id,Func<IQueryable<T>, IQueryable<T>>? include = null,CancellationToken cancellationToken = default)
        {
            IQueryable<T> query = _entities;

            if (include is not null)
                query = include(query);

            return await query.FirstOrDefaultAsync(e => e.Id == id, cancellationToken);
        }

        public void Remove(T entity)
        {
            entity.IsDeleted = true;
            entity.DeleteAt = DateTimeOffset.UtcNow;
            _entities.Update(entity);
        }
        public void Update(T entity)
        {
            _entities.Update(entity);
        }

        public async Task<bool> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            return await _context.SaveChangesAsync(cancellationToken) > 0;
        }
    }
}





