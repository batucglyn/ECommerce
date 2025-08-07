using ECommerce.Domain.Abstractions.Repositories;
using ECommerce.Domain.Entities.Orders;
using ECommerce.Domain.Entities.Products;
using ECommerce.Domain.UnitOfWork;
using ECommerce.Infrastructure.Context;
using ECommerce.Infrastructure.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.AccessControl;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.Infrastructure.UnitOfWorks
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly AppDbContext _context;

        public UnitOfWork(AppDbContext context)
        {
            _context = context;
            ProductRepository = new Repository<Product>(_context);
            OrderRepository = new Repository<Order>(_context);
        }

        public IRepository<Product> ProductRepository { get; }
        public IRepository<Order> OrderRepository { get; }

        public async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
             return await _context.SaveChangesAsync(cancellationToken);
        }
    }
}
