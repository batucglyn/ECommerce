using ECommerce.Domain.Abstractions.Repositories;
using ECommerce.Domain.Entities.Orders;
using ECommerce.Domain.Entities.Products;

namespace ECommerce.Domain.UnitOfWork
{
    public interface IUnitOfWork
    {
        IRepository<Product> ProductRepository { get; }
        IRepository<Order> OrderRepository { get; }

        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
    }
}
