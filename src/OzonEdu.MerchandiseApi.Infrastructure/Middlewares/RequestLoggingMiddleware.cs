using System;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using OzonEdu.MerchandiseApi.Infrastructure.Extensions;

namespace OzonEdu.MerchandiseApi.Infrastructure.Middlewares
{
    internal sealed class RequestLoggingMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<RequestLoggingMiddleware> _logger;

        private readonly string[] _skipSegments = new[]
        {
            "/swagger",
            "/MerchApi.MerchApiGrpc"
        };

        public RequestLoggingMiddleware(RequestDelegate next, ILogger<RequestLoggingMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            if (_skipSegments.Any(segment => context.Request.Path.StartsWithSegments(segment)))
            {
                await _next(context);
            }
            else
            {
                // TODO: возможно не самое оптимальное решение
                // см https://stackoverflow.com/a/52328142/2814287
                // https://docs.microsoft.com/ru-ru/aspnet/core/fundamentals/middleware/request-response?view=aspnetcore-5.0
                // + вложенные try-catch, исследовать
                // пока хватит и этого
                var originalResponseBodyStream = context.Response.Body;

                try
                {
                    using (var bufferedResponseBodyStream = new MemoryStream())
                    {
                        context.Response.Body = bufferedResponseBodyStream;

                        await LogRequest(context);
                        await _next(context);
                        await LogResponse(context);

                        bufferedResponseBodyStream.Position = 0;
                        await bufferedResponseBodyStream.CopyToAsync(originalResponseBodyStream);
                    }
                }
                catch
                {
                    throw;
                }
                finally
                {
                    context.Response.Body = originalResponseBodyStream;
                }
            }
        }

        private async Task LogRequest(HttpContext context)
        {
            try
            {
                var method = context.Request.Method;
                var path = context.Request.Path;
                var headers = context.Request.Headers.ReadHeadersAsString();
                var body = string.Empty;

                if (context.Request.ContentLength > 0)
                {
                    context.Request.EnableBuffering();
                    body = await context.Request.Body.ReadBodyAsString();
                }

                var sb = new StringBuilder();

                sb.AppendLine($"HTTP Request {method} {path}:");
                sb.AppendLine(headers);

                if (string.IsNullOrEmpty(body) == false)
                {
                    sb.AppendLine(body);
                }

                _logger.LogInformation(sb.ToString());
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Could not log HTTP request");
            }
        }

        private async Task LogResponse(HttpContext context)
        {
            try
            {
                var method = context.Request.Method;
                var path = context.Request.Path;
                var headers = context.Response.Headers.ReadHeadersAsString();
                var body = string.Empty;

                if (context.Response.Body.Length > 0)
                {
                    body = await context.Response.Body.ReadBodyAsString();
                }

                var sb = new StringBuilder();

                sb.AppendLine($"HTTP Response {method} {path}:");
                sb.AppendLine(headers);

                if (string.IsNullOrEmpty(body) == false)
                {
                    sb.AppendLine(body);
                }

                _logger.LogInformation(sb.ToString());
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Could not log HTTP response");
            }
        }
    }
}
