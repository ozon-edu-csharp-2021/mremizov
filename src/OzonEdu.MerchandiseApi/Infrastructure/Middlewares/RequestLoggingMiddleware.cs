using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using OzonEdu.MerchandiseApi.Infrastructure.Extensions;

namespace OzonEdu.MerchandiseApi.Infrastructure.Middlewares
{
    public class RequestLoggingMiddleware
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
            await LogRequest(context);
            await _next(context);
            await LogResponse(context);
        }

        private async Task LogRequest(HttpContext context)
        {
            try
            {
                if (_skipSegments.Any(segment => context.Request.Path.StartsWithSegments(segment)))
                {
                    return;
                }

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
                if (_skipSegments.Any(segment => context.Request.Path.StartsWithSegments(segment)))
                {
                    return;
                }

                var method = context.Request.Method;
                var path = context.Request.Path;
                var headers = context.Response.Headers.ReadHeadersAsString();
                var body = string.Empty;

                if (context.Response.ContentLength > 0)
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
