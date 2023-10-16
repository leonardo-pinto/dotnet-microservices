using Basket.Application.Handlers;
using Basket.Application.Repositories;
using Basket.Infrastructure.Repositories;
using Microsoft.OpenApi.Models;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using HealthChecks.UI.Client;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Basket.Application.GrpcService;
using Discount.Grpc.Protos;

namespace Basket.API
{
    public class Startup
    {
        public IConfiguration configuration;

        public Startup(IConfiguration configuration)
        {
            this.configuration = configuration;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();
            services.AddApiVersioning();
            // Redis registration
            services.AddStackExchangeRedisCache(opt =>
            {
                opt.Configuration = configuration.GetValue<string>("CacheSettings:ConnectionString");
            });
            services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblyContaining<CreateShoppingCartHandler>());
            services.AddScoped<IBasketRepository, BasketRepository>();
            services.AddAutoMapper(typeof(Startup));
            services.AddScoped<DiscountGrpcService>();
            services.AddGrpcClient<DiscountProtoService.DiscountProtoServiceClient>(
                o => o.Address = new Uri(configuration["GrpcSettings:DiscountUrl"]));

            services.AddSwaggerGen(options =>
            {
                options.SwaggerDoc("v1", new OpenApiInfo { Title = "Basket API", Version = "v1" });
            });

            services.AddHealthChecks().AddRedis(configuration["CacheSettings:ConnectionString"], "Redis Health", HealthStatus.Degraded);
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(o => o.SwaggerEndpoint("/swagger/v1/swagger.json", "Basket.API v1"));
            }

            app.UseRouting();
            app.UseHttpsRedirection();
            app.UseAuthorization();
            app.UseEndpoints(endpoint =>
            {
                endpoint.MapControllers();
                endpoint.MapHealthChecks("/health", new HealthCheckOptions()
                {
                    Predicate = _ => true,
                    ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse
                });
            });
        }
    }
}
