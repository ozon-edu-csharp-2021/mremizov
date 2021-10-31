using System;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using OzonEdu.MerchandiseApi.Infrastructure.Middlewares;

namespace OzonEdu.MerchandiseApi.Infrastructure.StartupFilters
{
    internal sealed class VersionStartupFilter : IStartupFilter
    {
        public Action<IApplicationBuilder> Configure(Action<IApplicationBuilder> next)
        {
            return app =>
            {
                app.Map("/version", builder => builder.UseMiddleware<VersionMiddleware>());

                next(app);
            };
        }
    }
}
