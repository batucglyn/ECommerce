using AutoMapper;
using ECommerce.Application.Exceptions;
using ECommerce.Application.Products.Rules;
using ECommerce.Domain.Abstractions.Repositories;
using ECommerce.Domain.Entities.Products;
using ECommerce.Domain.UnitOfWork;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.Application.Products.Commands
{
    public class CreateProductCommandHandler : IRequestHandler<CreateProductCommand, Guid>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IProductBusinessRules _businessRules;
        public CreateProductCommandHandler(IMapper mapper, IUnitOfWork unitOfWork, IProductBusinessRules businessRules)
        {

            _mapper = mapper;
            _unitOfWork = unitOfWork;
            _businessRules = businessRules;
        }

        public async Task<Guid> Handle(CreateProductCommand request, CancellationToken cancellationToken)
        {
            _businessRules.CheckStockGreaterThanZero(request.Stock);
            var product= _mapper.Map<Product>(request);
            await _unitOfWork.ProductRepository.AddAsync(product,cancellationToken);
            await _unitOfWork.SaveChangesAsync(cancellationToken);
            return product.Id;
        }
    }
   
}
