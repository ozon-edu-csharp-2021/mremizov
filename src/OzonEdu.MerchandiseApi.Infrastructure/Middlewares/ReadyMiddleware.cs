using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace OzonEdu.MerchandiseApi.Infrastructure.Middlewares
{
    internal sealed class ReadyMiddleware
    {
        public ReadyMiddleware(RequestDelegate next)
        {
        }

        public Task InvokeAsync(HttpContext context)
        {
            return context.Response.WriteAsync("OK");
        }
    }
}
