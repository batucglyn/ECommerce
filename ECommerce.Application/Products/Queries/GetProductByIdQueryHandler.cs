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
    public class GetProductByIdQueryHandler : IRequestHandler<GetProductByIdQuery, ProductDto>
    {
        private readonly IRepository<Product> _productRepository;
        private readonly IMapper _mapper;
        private readonly IProductBusinessRules _businessRules;

        public GetProductByIdQueryHandler(IMapper mapper, IRepository<Product> productRepository, IProductBusinessRules businessRules)
        {
            _mapper = mapper;
            _productRepository = productRepository;
            _businessRules = businessRules;
        }

        public async Task<ProductDto> Handle(GetProductByIdQuery request, CancellationToken cancellationToken)
        {


          var product=  await _productRepository.GetByIdAsync(request.Id, include: null, cancellationToken);

            _businessRules.CheckProductExists(product, request.Id);
            
            return _mapper.Map<ProductDto>(product);
        }
    }
}
