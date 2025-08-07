using ECommerce.Application.Exceptions;
using ECommerce.Domain.Entities.Products;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.Application.Products.Rules
{
    public class ProductBusinessRules : IProductBusinessRules
    {
        public void CheckProductExists(Product? product, Guid id)
        {
            if (product is null)
                throw new NotFoundException($"Ürün bulunamadı. Id: {id}");

        }

        public void CheckStockGreaterThanZero(int stock)
        {
            if (stock < 0)
                throw new BusinessRuleException("Stok miktarı 0'dan küçük olamaz.");
        }

        public void CheckAllProducts(IReadOnlyList<Product> products)
        {

            if (products == null || !products.Any())
            {
                throw new NotFoundException("Hiç ürün bulunamadı.");
            }

        }


        public void CheckActiveProducts(IReadOnlyList<Product> products)
        {
            if (!products.Any(p => !p.IsDeleted)) 
                throw new NotFoundException("Aktif  ürün bulunamadı.");

        }
    }
}
