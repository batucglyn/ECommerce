using ECommerce.Domain.Abstractions;
using ECommerce.Domain.Entities.OrderItems;
using ECommerce.Domain.Entities.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.Domain.Entities.Orders
{
    public sealed class Order:Entity
    {
        public Guid UserId { get; set; }
        public AppUser User { get; set; } = null!;
        public DateTime OrderDate { get; set; } = DateTime.UtcNow;
        public ICollection<OrderItem> OrderItems { get; set; } = new List<OrderItem>();
    }
}
