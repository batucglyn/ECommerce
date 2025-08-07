using ECommerce.Application.Orders.Dtos;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.Application.Orders.Commands
{
    public sealed record CreateOrderCommand(
  
     List<OrderItemDto> OrderItems
 ) : IRequest<Guid>;
}
