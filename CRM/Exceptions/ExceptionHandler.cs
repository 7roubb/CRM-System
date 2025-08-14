// ExceptionHandler.cs
using CRM.Exceptions;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace CRM.Middleware
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

                        switch (exception)
                        {
                            case UserNotFoundException:
                            case ContactNotFoundException:
                                problemDetails.Status = (int)HttpStatusCode.NotFound;
                                problemDetails.Title = "Resource Not Found";
                                logger.LogWarning(exception, "Resource not found: {Message}", exception.Message);
                                break;

                            case UserAlreadyExistsException:
                            case ContactAlreadyExistsException: 
                                problemDetails.Status = (int)HttpStatusCode.Conflict;
                                problemDetails.Title = "Duplicate Resource";
                                logger.LogWarning(exception, "Duplicate resource: {Message}", exception.Message);
                                break;

                            case AuthenticationFailedException:
                            case InvalidRoleException:
                            case ValidationException:
                                problemDetails.Status = (int)HttpStatusCode.BadRequest;
                                problemDetails.Title = "Invalid Request";
                                logger.LogWarning(exception, "Validation error: {Message}", exception.Message);
                                break;

                            case ConfigurationException:
                            case DatabaseOperationException:
                          
                            default:
                                problemDetails.Status = (int)HttpStatusCode.InternalServerError;
                                problemDetails.Title = "Internal Server Error";
                                logger.LogError(exception, "Unhandled exception: {Message}", exception.Message);
                                break;
                        }

                        problemDetails.Detail = exception.Message;

                        context.Response.StatusCode = problemDetails.Status.Value;
                        context.Response.ContentType = "application/problem+json";

                        var env = context.RequestServices.GetRequiredService<IWebHostEnvironment>();
                        if (env.IsDevelopment())
                        {
                            problemDetails.Extensions["exception"] = new
                            {
                                Type = exception.GetType().Name,
                                exception.StackTrace
                            };
                        }

                        await context.Response.WriteAsJsonAsync(problemDetails);
                    }
                });
            });
        }
    }
}