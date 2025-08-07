using AutoMapper;
using ECommerce.Application.Exceptions;
using ECommerce.Application.Products.Dtos;
using ECommerce.Application.Products.Rules;
using ECommerce.Domain.Abstractions.Repositories;
using ECommerce.Domain.Entities.Products;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.Application.Products.Queries
{
    public class GetAllProductsQueryHandler : IRequestHandler<GetAllProductsQuery, IReadOnlyList<ProductDto>>
    {
        private readonly IRepository<Product> _productRepository;
        private readonly IMapper _mapper;
        private readonly IProductBusinessRules _businessRules;
        public GetAllProductsQueryHandler(IMapper mapper, IRepository<Product> productRepository, IProductBusinessRules businessRules)
        {
            _mapper = mapper;
            _productRepository = productRepository;
            _businessRules = businessRules;
        }

        public async Task<IReadOnlyList<ProductDto>> Handle(GetAllProductsQuery request, CancellationToken cancellationToken)
        {

            var allProducts = await _productRepository.GetAllAsync(include:null,cancellationToken);

            _businessRules.CheckAllProducts(allProducts);

           

            var activeProducts = allProducts.Where(p => !p.IsDeleted).ToList();

          _businessRules.CheckActiveProducts(activeProducts);

            return _mapper.Map<IReadOnlyList<ProductDto>>(activeProducts);



        }
    }
}
