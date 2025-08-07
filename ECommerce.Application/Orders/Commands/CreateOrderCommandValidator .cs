using FluentValidation;
using FluentValidation.Validators;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.Application.Orders.Commands
{
    public class CreateOrderCommandValidator : AbstractValidator<CreateOrderCommand>
    {
        public CreateOrderCommandValidator()
        {
            
            RuleFor(x => x.OrderItems).NotEmpty().WithMessage("Siparişte en az 1 ürün olmalıdır.");

            RuleForEach(x => x.OrderItems).ChildRules(items =>
            {
                items.RuleFor(i => i.ProductId).NotEmpty();
                items.RuleFor(i => i.Quantity).GreaterThan(0);
                items.RuleFor(i => i.Price).GreaterThan(0);
            });
        }
    }
}
