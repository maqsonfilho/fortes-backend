using Fortes.Web.Challenge.Application.Validators;

namespace Fortes.Web.Challenge.Api.Extensions;

public static class ValidatorsInjectionExtension
{
    public static void RegisterValidatorsDependencies(this IServiceCollection services)
    {
        services.AddTransient<CreateSupplierValidator>();
        services.AddTransient<CreateProductValidator>();
        services.AddTransient<CreateOrderValidator>();
    }
}
