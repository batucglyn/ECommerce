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
    public sealed class DeleteProductCommandHandler : IRequestHandler<DeleteProductCommand>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IProductBusinessRules _businessRules;

        public DeleteProductCommandHandler(IUnitOfWork unitOfWork, IProductBusinessRules businessRules)
        {
            _unitOfWork = unitOfWork;
            _businessRules = businessRules;
        }



        public async Task Handle(DeleteProductCommand request, CancellationToken cancellationToken)
        {
            var product = await _unitOfWork.ProductRepository.GetByIdAsync(request.Id,include:null, cancellationToken);
            _businessRules.CheckProductExists(product, request.Id);

            _unitOfWork.ProductRepository.Remove(product); // Soft delete
            await _unitOfWork.SaveChangesAsync(cancellationToken);
        }
    }
}
