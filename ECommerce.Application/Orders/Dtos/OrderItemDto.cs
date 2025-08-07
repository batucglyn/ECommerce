using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.Application.Orders.Dtos
{
    public sealed class OrderItemDto
    {
        public Guid ProductId { get; set; }
        public string ProductName { get; set; } = string.Empty; // src.Product.Name
        public int Quantity { get; set; }
        public decimal Price { get; set; }
    }
}
