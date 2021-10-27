using System;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Grpc.Core;
using Grpc.Core.Interceptors;
using Microsoft.Extensions.Logging;

namespace OzonEdu.MerchandiseApi.Infrastructure.Interceptors
{
    public class LoggingInterceptor : Interceptor
    {
        private readonly ILogger<LoggingInterceptor> _logger;

        public LoggingInterceptor(ILogger<LoggingInterceptor> logger)
        {
            _logger = logger;
        }

        public override Task<TResponse> UnaryServerHandler<TRequest, TResponse>(TRequest request,
            ServerCallContext context,
            UnaryServerMethod<TRequest, TResponse> continuation)
        {
            LogRequest(context, request);
            var response = base.UnaryServerHandler(request, context, continuation);
            LogResponse(context, response);

            return response;
        }

        private void LogRequest<TRequest>(ServerCallContext context, TRequest request)
        {
            try
            {
                var requestJson = JsonSerializer.Serialize(request);

                var sb = new StringBuilder();

                sb.AppendLine($"GRPC Request {context.Method}:");
                sb.AppendLine(requestJson);

                _logger.LogInformation(sb.ToString());
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Could not log GRPC request");
            }
        }

        private void LogResponse<TResponse>(ServerCallContext context, TResponse response)
        {
            try
            {
                var responseJson = JsonSerializer.Serialize(response);

                var sb = new StringBuilder();

                sb.AppendLine($"GRPC Response {context.Method}:");
                sb.AppendLine(responseJson);

                _logger.LogInformation(sb.ToString());
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Could not log GRPC response");
            }
        }
    }
}
