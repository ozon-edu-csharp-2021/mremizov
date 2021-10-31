using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace OzonEdu.MerchandiseApi.Infrastructure.Middlewares
{
    internal sealed class LiveMiddleware
    {
        public LiveMiddleware(RequestDelegate next)
        {
        }

        public Task InvokeAsync(HttpContext context)
        {
            return context.Response.WriteAsync("OK");
        }
    }
}
