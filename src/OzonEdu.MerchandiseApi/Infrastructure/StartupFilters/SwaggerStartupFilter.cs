using System;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;

namespace OzonEdu.MerchandiseApi.Infrastructure.StartupFilters
{
    public class SwaggerStartupFilter : IStartupFilter
    {
        private readonly IWebHostEnvironment _environment;

        public SwaggerStartupFilter(IWebHostEnvironment environment)
        {
            _environment = environment;
        }

        public Action<IApplicationBuilder> Configure(Action<IApplicationBuilder> next)
        {
            return app =>
            {
                if (_environment.IsDevelopment())
                {
                    app.UseSwagger();
                    app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "OzonEdu.MerchandiseApi v1"));
                }

                next(app);
            };
        }
    }
}
