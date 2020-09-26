using AutoMapper;
using CurrencyData.Infrastructure.Mapping;
using CurrencyData.Infrastructure.Repositories;
using CurrencyData.Infrastructure.Repositories.Abstractions;
using CurrencyData.Infrastructure.Services;
using CurrencyData.Infrastructure.Services.Abstractions;
using EcbSdmx.Infrastructure.Services;
using EcbSdmx.Infrastructure.Services.Abstractions;
using Microsoft.Extensions.DependencyInjection;

namespace CurrencyData.Infrastructure.Extensions
{
    public static class ServiceCollectionExtension
    {
        public static IServiceCollection AddCurrencyDataInfrastructure(this IServiceCollection services)
        {
            // Caching
            services.AddDistributedMemoryCache();

            // Mapping
            services.AddAutoMapper(typeof(MappingProfile));

            // Services
            services.AddScoped<ICurrencyDataService, CurrencyDataService>();
            services.AddHttpClient<IEcbSdmxService, EcbSdmxService>();

            // Repositories
            services.AddScoped<ICurrencyDataRepository, CurrencyDataRepository>();

            return services;
        }
    }
}
