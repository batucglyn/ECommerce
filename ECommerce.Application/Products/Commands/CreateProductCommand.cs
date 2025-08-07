using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.Application.Products.Commands
{
    public  record  CreateProductCommand(
        
        string Name,decimal Price,int Stock, string Description



        ) :IRequest<Guid>;
        
            
    
}

  
   