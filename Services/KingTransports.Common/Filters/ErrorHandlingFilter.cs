using KingTransports.Common.Errors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Logging;
using System.Net;

namespace KingTransports.Common.Filters
{
    public class ErrorHandlingFilter : IExceptionFilter
    {
        private readonly ILogger _logger;

        public ErrorHandlingFilter(ILogger<ErrorHandlingFilter> logger)
        {
            _logger = logger;
        }

        public void OnException(ExceptionContext context)
        {
            if (!context.ExceptionHandled)
            {
                if (context.Exception is NotFound)
                {
                    context.Result = new ObjectResult(new
                    {
                        StatusCode = HttpStatusCode.NotFound,
                        Error = context.Exception.Message,
                        ErrorCode = context.Exception.Message
                    });
                }

                else if (context.Exception is ProcessingProblem)
                {
                    context.Result = new ObjectResult(new
                    {
                        StatusCode = HttpStatusCode.InternalServerError,
                        Error = context.Exception.Message,
                        ErrorCode = context.Exception.Message
                    });
                }
                else if (context.Exception is InvalidRequest)
                {
                    context.Result = new ObjectResult(new
                    {
                        StatusCode = HttpStatusCode.BadRequest,
                        Error = context.Exception.Message,
                        ErrorCode = context.Exception.Message
                    });
                }
                else
                {
                    context.Result = new ObjectResult(new
                    {
                        StatusCode = HttpStatusCode.InternalServerError,
                        Error = "Something went wrong :(",
                        ErrorCode = "internal_server_error"
                    });
                }

                _logger.LogError(context.Exception.ToString());
                context.ExceptionHandled = true;
            }
        }
    }
}