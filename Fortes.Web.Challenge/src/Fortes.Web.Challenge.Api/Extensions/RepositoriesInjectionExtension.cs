using Fortes.Web.Challenge.Domain.Core.Interfaces.Repositories;
using Fortes.Web.Challenge.Infrastructure.Repositories;

namespace Fortes.Web.Challenge.Api.Extensions;

public static class RepositoriesInjectionExtension
{
    public static void RegisterRepositoriesDependencies(this IServiceCollection services)
    {
        services.AddScoped<ISupplierRepository, SupplierRepository>();
        services.AddScoped<IProductRepository, ProductRepository>();
        services.AddScoped<IOrderRepository, OrderRepository>();
    }
}
