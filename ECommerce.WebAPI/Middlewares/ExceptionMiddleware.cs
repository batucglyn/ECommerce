using ECommerce.Application.Exceptions;
using FluentValidation;
using System.Net;
using System.Text.Json;

namespace ECommerce.WebAPI.Middlewares
{
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate _next;

        public ExceptionMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                await HandleExceptionAsync(context, ex);
            }
        }

        private static Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            context.Response.ContentType = "application/json";

            var statusCode = exception switch
            {
                NotFoundException => HttpStatusCode.NotFound,
                BusinessRuleException => HttpStatusCode.BadRequest,
                ValidationException => HttpStatusCode.BadRequest,
                _ => HttpStatusCode.InternalServerError
            };

            context.Response.StatusCode = (int)statusCode;

            var errorResponse = new
            {
                error = exception.Message,
                status = context.Response.StatusCode
            };

            return context.Response.WriteAsync(JsonSerializer.Serialize(errorResponse));
        }
    }
}
