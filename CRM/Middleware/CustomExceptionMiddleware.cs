
using System.Net;
using System.Text.Json;
using CRM.Exceptions;

namespace CRM.Middleware
{
    public class CustomExceptionMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<CustomExceptionMiddleware> _logger;

        public CustomExceptionMiddleware(RequestDelegate next, ILogger<CustomExceptionMiddleware> logger)
        {
            _next = next;
            _logger = logger;
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

        private Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            context.Response.ContentType = "application/json";

            var statusCode = (int)HttpStatusCode.InternalServerError;
            var message = "An unexpected error occurred.";

            switch (exception)
            {
                case UserAlreadyExistsException ex:
                    statusCode = (int)HttpStatusCode.Conflict; 
                    message = ex.Message;
                    _logger.LogWarning($"User already exists: {ex.Message}");
                    break;

                case ResourceNotFoundException ex:
                    statusCode = (int)HttpStatusCode.NotFound; 
                    message = ex.Message;
                    _logger.LogWarning($"Resource not found: {ex.Message}");
                    break;

                case AuthenticationFailedException ex:
                    statusCode = (int)HttpStatusCode.Unauthorized; 
                    message = ex.Message;
                    _logger.LogWarning($"Authentication failed: {ex.Message}");
                    break;

                case DatabaseOperationException ex:
                    statusCode = (int)HttpStatusCode.InternalServerError; 
                    message = ex.Message;
                    _logger.LogError(ex, $"Database operation failed: {ex.Message}");
                    break;

                case ConfigurationException ex:
                    statusCode = (int)HttpStatusCode.InternalServerError; 
                    message = ex.Message;
                    _logger.LogError(ex, $"Configuration error: {ex.Message}");
                    break;

                default:
                    _logger.LogError(exception, $"Unhandled exception: {exception.Message}");
                    break;
            }

            var result = JsonSerializer.Serialize(new
            {
                status = statusCode,
                message
            });

            context.Response.StatusCode = statusCode;
            return context.Response.WriteAsync(result);
        }
    }
}