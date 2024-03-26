using AutoMapper;
using Fortes.Web.Challenge.Application.MappingProfiles;
using Fortes.Web.Challenge.Infrastructure.Data.Contexts;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Fortes.Web.Challenge.Api.Extensions;

public static class ServiceExtension
{
    public static void ConfigureLogger(this IServiceCollection services)
        => services.AddLogging();

    public static void ConfigureMapping(this IServiceCollection services)
    {
        services.Configure<ApiBehaviorOptions>(options =>
        {
            options.SuppressModelStateInvalidFilter = true;
        });

        var mapperConfig = new MapperConfiguration(map =>
        {
            map.AddProfile<ProductMappingProfile>();
            map.AddProfile<SupplierMappingProfile>();
            map.AddProfile<OrderMappingProfile>();
        });

        services.AddSingleton(mapperConfig.CreateMapper());
    }

    public static void AddContext(this IServiceCollection services, IConfiguration configuration)
    {
        string connectionString = configuration.GetConnectionString("FortesDbContext")!;

        services.AddDbContext<FortesDbContext>(options => options.UseSqlServer(connectionString,
            opt => opt.MigrationsAssembly("Fortes.Web.Challenge.Infrastructure.Data")));
    }
}
