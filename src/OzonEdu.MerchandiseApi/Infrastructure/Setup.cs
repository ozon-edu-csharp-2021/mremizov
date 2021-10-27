using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using OzonEdu.MerchandiseApi.Infrastructure.Filters;
using OzonEdu.MerchandiseApi.Infrastructure.Interceptors;
using OzonEdu.MerchandiseApi.Infrastructure.StartupFilters;

namespace OzonEdu.MerchandiseApi.Infrastructure
{
    public static class Setup
    {
        public static IHostBuilder AddInfrastructure(this IHostBuilder builder)
        {
            return builder.ConfigureServices(services =>
            {
                services.AddControllers(options => options.Filters.Add<GlobalExceptionFilter>());
                services.AddGrpc(options => options.Interceptors.Add<LoggingInterceptor>());

                services.AddSwaggerGen(c =>
                {
                    c.SwaggerDoc("v1", new OpenApiInfo { Title = "OzonEdu.MerchandiseApi", Version = "v1" });
                });

                services.AddSingleton<IStartupFilter, VersionStartupFilter>();
                services.AddSingleton<IStartupFilter, ReadyLiveStartupFilter>();
                services.AddSingleton<IStartupFilter, RequestLoggingStartupFilter>();
                services.AddSingleton<IStartupFilter, SwaggerStartupFilter>();
            });
        }
    }
}
