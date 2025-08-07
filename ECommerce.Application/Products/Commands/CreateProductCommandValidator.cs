using FluentValidation;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.Application.Products.Commands
{
    public class CreateProductCommandValidator : AbstractValidator<CreateProductCommand>
    {
        public CreateProductCommandValidator()
        {
            RuleFor(x=>x.Name).NotEmpty().MaximumLength(80);
            RuleFor(x=>x.Price).GreaterThan(0);
            RuleFor(x=>x.Stock).GreaterThanOrEqualTo(0);
            RuleFor(x=>x.Description).MaximumLength(200);
        }
    }
}
