using System;
using System.IO;
using System.Reflection;
using CurrencyData.Api.Repositories;
using CurrencyData.Api.Repositories.Abstractions;
using CurrencyData.Api.Services;
using CurrencyData.Api.Services.Abstractions;
using EcbSdmx.Infrastructure.Services;
using EcbSdmx.Infrastructure.Services.Abstractions;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace CurrencyData.Api
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();

            // Caching
            services.AddResponseCaching();
            services.AddDistributedMemoryCache();

            // Services
            services.AddScoped<ICurrencyDataService, CurrencyDataService>();
            services.AddHttpClient<IEcbSdmxService, EcbSdmxService>();

            // Repositories
            services.AddScoped<ICurrencyDataRepository, CurrencyDataRepository>();

            // Register Swagger
            services.AddSwaggerGen(c =>
            {
                // Set the comments path for the Swagger JSON and UI.
                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                c.IncludeXmlComments(xmlPath);
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            // Enable Swagger and JSON endpoint
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Currency Data Api V1");
                c.RoutePrefix = string.Empty;
            });

            app.UseHttpsRedirection();
            app.UseRouting();
            app.UseAuthorization();
            app.UseResponseCaching();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
