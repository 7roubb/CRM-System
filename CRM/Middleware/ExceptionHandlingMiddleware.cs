using CRM.Config;
using CRM.Exceptions;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System;
using System.Net;
using System.Threading.Tasks;

namespace CRM.Middleware
{
    public class ExceptionHandlingMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ExceptionHandlingMiddleware> _logger;

        public ExceptionHandlingMiddleware(RequestDelegate next, ILogger<ExceptionHandlingMiddleware> logger)
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
                _logger.LogError(ex, "Unhandled exception occurred");

                var response = context.Response;
                response.ContentType = "application/json";

                var status = HttpStatusCode.InternalServerError;
                string message = "An unexpected error occurred.";

                switch (ex)
                {
                    case UserNotFoundException:
                        status = HttpStatusCode.NotFound;
                        message = ex.Message;
                        break;

                    case UserAlreadyExistsException:
                        status = HttpStatusCode.Conflict;
                        message = ex.Message;
                        break;

                    case InvalidRoleException:
                    case ValidationException:
                        status = HttpStatusCode.BadRequest;
                        message = ex.Message;
                        break;

                    case DatabaseOperationException:
                        status = HttpStatusCode.ServiceUnavailable;
                        message = ex.Message;
                        break;
                }

                var apiResponse = ApiResponse<string>.Error(message, status);

                response.StatusCode = (int)status;
                await response.WriteAsJsonAsync(apiResponse);
            }
        }
    }
}
