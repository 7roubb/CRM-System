using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace CRM.Exceptions
{
    public static class ExceptionHandler
    {
        public static void UseCustomExceptionHandler(this IApplicationBuilder app)
        {
            app.UseExceptionHandler(errorApp =>
            {
                errorApp.Run(async context =>
                {
                    var contextFeature = context.Features.Get<IExceptionHandlerFeature>();
                    if (contextFeature?.Error != null)
                    {
                        var exception = contextFeature.Error;
                        var problemDetails = new ProblemDetails();
                        var logger = context.RequestServices.GetRequiredService<ILogger<Program>>();

                        // Log the exception
                        logger.LogError(exception, "An unhandled exception occurred: {Message}", exception.Message);

                        // Handle specific exception types
                        switch (exception)
                        {
                            case UserNotFoundException:
                            case ResourceNotFoundException:
                                problemDetails.Status = (int)HttpStatusCode.NotFound;
                                problemDetails.Title = "Resource Not Found";
                                break;

                            case UserAlreadyExistsException:
                                problemDetails.Status = (int)HttpStatusCode.Conflict;
                                problemDetails.Title = "Duplicate Resource";
                                break;

                            case InvalidRoleException:
                            case ValidationException:
                            case AuthenticationFailedException:
                                problemDetails.Status = (int)HttpStatusCode.BadRequest;
                                problemDetails.Title = "Invalid Request";
                                break;

                            case ConfigurationException:
                            case DatabaseOperationException:
                                problemDetails.Status = (int)HttpStatusCode.InternalServerError;
                                problemDetails.Title = "Server Error";
                                break;

                            default:
                                problemDetails.Status = (int)HttpStatusCode.InternalServerError;
                                problemDetails.Title = "Internal Server Error";
                                break;
                        }

                        problemDetails.Detail = exception.Message;
                        context.Response.StatusCode = problemDetails.Status.Value;
                        context.Response.ContentType = "application/problem+json";

                        // Add additional details for development environment
                        var env = context.RequestServices.GetRequiredService<IWebHostEnvironment>();
                        if (env.IsDevelopment())
                        {
                            problemDetails.Extensions["exception"] = new
                            {
                                Type = exception.GetType().Name,
                                StackTrace = exception.StackTrace
                            };
                        }

                        await context.Response.WriteAsJsonAsync(problemDetails);
                    }
                });
            });
        }
    }
}