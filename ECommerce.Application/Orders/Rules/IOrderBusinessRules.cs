using ECommerce.Domain.Entities.Orders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.Application.Orders.Rules
{
    public interface IOrderBusinessRules
    {

        void CheckOrderExists(Order? order, Guid id);
        void CheckAllOrders(IReadOnlyList<Order> orders);

    }
}
