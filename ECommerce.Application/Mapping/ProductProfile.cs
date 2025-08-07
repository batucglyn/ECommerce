using AutoMapper;
using ECommerce.Application.Products.Commands;
using ECommerce.Application.Products.Dtos;
using ECommerce.Domain.Entities.Products;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.Application.Mapping
{
    public sealed class ProductProfile : Profile
    {
        public ProductProfile()
        {
            CreateMap<CreateProductCommand, Product>();
            CreateMap<Product, ProductDto>();
          
        }
    }
}
