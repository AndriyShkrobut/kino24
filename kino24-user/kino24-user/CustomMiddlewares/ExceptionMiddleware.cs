using System.Net;
using kino24_user.BL.Interfaces.Logging;
using kino24_user.Models.ErrorHandling;

namespace kino24_user.CustomMiddlewares
{
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly IGlobalLoggerService _logger;

        public ExceptionMiddleware(RequestDelegate next, IGlobalLoggerService logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task InvokeAsync(HttpContext httpContext)
        {
            try
            {
                await _next(httpContext);
            }
            catch (UnauthorizedAccessException ex)
            {
                _logger.LogError(ex);
                await HandleExceptionAsync(httpContext, HttpStatusCode.Unauthorized, "Unauthorized");
            }
            catch (NullReferenceException ex)
            {
                _logger.LogError(ex);
                await HandleExceptionAsync(httpContext, HttpStatusCode.NotFound, "Not Found");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex);
                await HandleExceptionAsync(httpContext, HttpStatusCode.BadRequest, "Bad Request");
            }
        }

        private Task HandleExceptionAsync(HttpContext context, HttpStatusCode statusCode, string message)
        {
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)statusCode;

            return context.Response.WriteAsync(new ErrorDetails()
            {
                StatusCode = context.Response.StatusCode,
                Message = message
            }.ToString());
        }
    }
}
