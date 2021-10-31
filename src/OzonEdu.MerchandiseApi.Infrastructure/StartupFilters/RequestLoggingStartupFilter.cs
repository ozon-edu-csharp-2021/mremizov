using System;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using OzonEdu.MerchandiseApi.Infrastructure.Middlewares;

namespace OzonEdu.MerchandiseApi.Infrastructure.StartupFilters
{
    internal sealed class RequestLoggingStartupFilter : IStartupFilter
    {
        public Action<IApplicationBuilder> Configure(Action<IApplicationBuilder> next)
        {
            return app =>
            {
                app.UseMiddleware<RequestLoggingMiddleware>();

                next(app);
            };
        }
    }
}
