using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.Application.Orders.Dtos
{
    public sealed class OrderDto
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public string FullName { get; set; } = string.Empty; // src.User.FullName
        public DateTime OrderDate { get; set; }
        public List<OrderItemDto> OrderItems { get; set; } = new();
    }
}

