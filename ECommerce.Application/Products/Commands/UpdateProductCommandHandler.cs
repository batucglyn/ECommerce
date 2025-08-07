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
    public class UpdateProductCommandHandler : IRequestHandler<UpdateProductCommand>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IProductBusinessRules _productBusinessRules;

        public UpdateProductCommandHandler(IUnitOfWork unitOfWork, IProductBusinessRules productBusinessRules)
        {

            _unitOfWork = unitOfWork;
            _productBusinessRules = productBusinessRules;
        }

        public async Task Handle(UpdateProductCommand request, CancellationToken cancellationToken)
        {
            var product= await _unitOfWork.ProductRepository.GetByIdAsync(request.Id,include:null,cancellationToken);
            _productBusinessRules.CheckProductExists(product,request.Id);

            product.Name = request.Name;
            product.Description = request.Description;
            product.Price=request.Price;
            product.Stock = request.Stock;
            product.IsActive=request.IsActive;
            product.UpdateAt = DateTime.Now;
            _unitOfWork.ProductRepository.Update(product);
            await _unitOfWork.SaveChangesAsync(cancellationToken);
        }
    }
}
