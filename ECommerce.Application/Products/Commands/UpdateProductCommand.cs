using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.Application.Products.Commands
{
    public record UpdateProductCommand(
        Guid Id,
        bool IsActive,
        string Name,
        string Description,
        decimal Price,
        int Stock) : IRequest;





}
