using System.Net;
using System.Text.Json;
using WasteBinsAPI.Exceptions;
using WasteBinsAPI.Models;

namespace WasteBinsAPI.Middleware;

public class ExceptionMiddleware(RequestDelegate next)
{
    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await next(context);
        }
        catch (Exception ex)
        {
            var statusCode = GetStatusCode(ex);
            await HandleExceptionResponseAsync(context, statusCode, ex);
        }
    }

    private static Task HandleExceptionResponseAsync(HttpContext context, HttpStatusCode statusCode, Exception ex)
    {
        context.Response.ContentType = "application/json";
        context.Response.StatusCode = (int)statusCode;

        var result = JsonSerializer.Serialize(new ApiError
        {
            Status = (int)statusCode,
            Message = ex.Message
        });

        return context.Response.WriteAsync(result);
    }

    private HttpStatusCode GetStatusCode(Exception exception)
    {
        return exception switch
        {
            ArgumentNullException => HttpStatusCode.BadRequest,
            UnauthorizedAccessException => HttpStatusCode.Unauthorized,
            KeyNotFoundException => HttpStatusCode.NotFound,
            UserAlreadyExists => HttpStatusCode.Conflict,
            _ => HttpStatusCode.InternalServerError
        };
    }
}