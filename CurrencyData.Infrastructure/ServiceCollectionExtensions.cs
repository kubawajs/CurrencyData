using AutoMapper;
using CurrencyData.Infrastructure.Mapping;
using CurrencyData.Infrastructure.Repositories;
using CurrencyData.Infrastructure.Repositories.Abstractions;
using CurrencyData.Infrastructure.Repositories.Mocks;
using CurrencyData.Infrastructure.Services;
using CurrencyData.Infrastructure.Services.Abstractions;
using EcbSdmx.Infrastructure.Services;
using EcbSdmx.Infrastructure.Services.Abstractions;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace CurrencyData.Infrastructure
{
    public static class ServiceCollectionExtension
    {
        public static IServiceCollection AddCurrencyDataInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            // Caching
            services.AddDistributedSqlServerCache(options =>
            {
                options.ConnectionString = configuration[Constants.Configuration.DistributedCache.ConnectionString];
                options.SchemaName = "dbo";
                options.TableName = configuration[Constants.Configuration.DistributedCache.TableName];
            });

            // Mapping
            services.AddAutoMapper(typeof(MappingProfile));

            // Services
            services.AddScoped<IAuthenticationService, AuthenticationService>();
            services.AddScoped<ICurrencyDataService, CurrencyDataService>();
            services.AddHttpClient<IEcbSdmxService, EcbSdmxService>();

            // Repositories
            services.AddScoped<ICurrencyDataRepository, CurrencyDataRepository>();
            services.AddScoped<IUserRepository, StaticUserRepository>();

            return services;
        }
    }
}
