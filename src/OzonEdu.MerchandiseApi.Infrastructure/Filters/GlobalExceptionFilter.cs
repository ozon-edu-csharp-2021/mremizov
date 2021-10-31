using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace OzonEdu.MerchandiseApi.Infrastructure.Filters
{
    internal sealed class GlobalExceptionFilter : ExceptionFilterAttribute
    {
        public override void OnException(ExceptionContext context)
        {
            var value = new
            {
                Type = context.Exception.GetType().FullName,
                Message = context.Exception.Message,
                StackTrace = context.Exception.StackTrace
            };

            var jsonResult = new JsonResult(value)
            {
                StatusCode = StatusCodes.Status500InternalServerError
            };

            context.Result = jsonResult;
        }
    }
}
