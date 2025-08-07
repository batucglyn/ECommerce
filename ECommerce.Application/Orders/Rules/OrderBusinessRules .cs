using ECommerce.Application.Exceptions;
using ECommerce.Domain.Entities.Orders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.Application.Orders.Rules
{
    public class OrderBusinessRules : IOrderBusinessRules
    {
        public void CheckAllOrders(IReadOnlyList<Order> orders)
        {
            if (orders == null || !orders.Any())
                throw new NotFoundException("Hiç sipariş bulunamadı.");
        }

        public void CheckOrderExists(Order? order, Guid id)
        {
            if (order == null)
                throw new NotFoundException($"Sipariş bulunamadı. Id: {id}");
        }
    }
}
