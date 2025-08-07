using ECommerce.Domain.Abstractions.Repositories;
using ECommerce.Domain.UnitOfWork;
using ECommerce.Infrastructure.Repositories;
using Microsoft.Extensions.DependencyInjection;
using ECommerce.Infrastructure.UnitOfWorks;




namespace ECommerce.Infrastructure
{
    /// <summary>
    /// Registers the dependencies related to the Infrastructure layer into the DI container.
    /// Adds the generic repository implementation to the service collection.
    /// </summary>
    public static class InfrastructureRegistrar
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services)
        {
            services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            return services;
     

        }
    }
}
