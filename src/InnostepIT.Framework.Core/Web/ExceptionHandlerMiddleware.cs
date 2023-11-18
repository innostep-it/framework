using System.ComponentModel.DataAnnotations;
using System.Net;
using System.Text.Json;
using InnostepIT.Framework.Core.Contract.Exceptions;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

namespace InnostepIT.Framework.Core.Web;

public class ExceptionHandlerMiddleware
{
    private readonly ILogger<ExceptionHandlerMiddleware> _logger;
    private readonly RequestDelegate _next;

    public ExceptionHandlerMiddleware(RequestDelegate next, ILogger<ExceptionHandlerMiddleware> logger)
    {
        _next = next;
        _logger = logger;
    }

    public async Task Invoke(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (Exception exception)
        {
            _logger.LogWarning(exception, "{MiddlewareName} catched exception.",
                typeof(ExceptionHandlerMiddleware).ToString());

            var response = context.Response;
            response.ContentType = "application/json";
            var result = JsonSerializer.Serialize(new
            {
                message = exception?.Message
            });

            switch (exception)
            {
                case ValidationException:
                    response.StatusCode = (int)HttpStatusCode.BadRequest;
                    break;
                case EntityNotFoundException e1:
                case KeyNotFoundException e2:
                    response.StatusCode = (int)HttpStatusCode.NotFound;
                    break;
                default:
                    response.StatusCode = (int)HttpStatusCode.InternalServerError;
                    result =
                        "Something on serverside went wrong. Please contact system administrator for further details.";
                    break;
            }

            await response.WriteAsync(result);
        }
    }
}