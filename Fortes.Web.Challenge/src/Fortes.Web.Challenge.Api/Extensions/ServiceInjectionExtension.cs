using Fortes.Web.Challenge.Application.Services;
using Fortes.Web.Challenge.Domain.Core.Interfaces.Services;


namespace Action.Web.Backoffice.Api.Extensions;

public static class ServicesInjectionExtension
{
    public static void RegisterServicesDependencies(this IServiceCollection services)
    {
        services.AddScoped<ISupplierService, SupplierService>();
        services.AddScoped<IProductService, ProductService>();
        services.AddScoped<IOrderService, OrderService>();
    }
}
