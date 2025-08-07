using ECommerce.Domain.Entities.Products;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.Application.Products.Rules
{
    public interface IProductBusinessRules
    {

        void CheckStockGreaterThanZero(int stock);
        void CheckProductExists(Product? product, Guid id);
        void CheckAllProducts(IReadOnlyList<Product> products);
        void CheckActiveProducts(IReadOnlyList<Product> products);
    }
}
