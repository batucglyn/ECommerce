using ECommerce.Application.Orders.Rules;
using ECommerce.Application.Products.Rules;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.Application
{
    public static class ApplicationRegistrar
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            services.AddScoped<IProductBusinessRules, ProductBusinessRules>();
            services.AddScoped<IOrderBusinessRules, OrderBusinessRules>();
            return services;
        }
    }
}
