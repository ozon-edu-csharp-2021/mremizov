using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Npgsql;
using OzonEdu.MerchandiseApi.Domain.Repositories;
using OzonEdu.MerchandiseApi.Domain.Repositories.Infrastructure;
using OzonEdu.MerchandiseApi.Domain.Services;
using OzonEdu.MerchandiseApi.Domain.Services.Mocks;
using OzonEdu.MerchandiseApi.GrpcServices;

namespace OzonEdu.MerchandiseApi
{
    public sealed class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddRepositories();
            services.AddServices();

            services.Configure<DbConfiguration>(Configuration.GetSection(nameof(DbConfiguration)));
            services.AddScoped<IDbConnectionFactory<NpgsqlConnection>, NpgsqlConnectionFactory>();

            // TODO: заменить на настоящие
            services.AddTransient<IEmailServiceMock, EmailServiceMock>();
            services.AddTransient<IEmployeeApiClientMock, EmployeeApiClientMock>();
            services.AddTransient<IStockApiClientMock, StockApiClientMock>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapGrpcService<MerchApiGrpService>();
                endpoints.MapControllers();
            });
        }
    }
}
