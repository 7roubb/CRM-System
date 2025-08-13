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

                        switch (exception)
                        {
                            case UserNotFoundException:
                                problemDetails.Status = (int)HttpStatusCode.NotFound;
                                problemDetails.Title = "Resource Not Found";
                                break;

                            case UserAlreadyExistsException:
                                problemDetails.Status = (int)HttpStatusCode.Conflict;
                                problemDetails.Title = "Duplicate Resource";
                                break;

                            case InvalidRoleException:
                            case ValidationException:
                                problemDetails.Status = (int)HttpStatusCode.BadRequest;
                                problemDetails.Title = "Invalid Request";
                                break;

                            case DatabaseOperationException:
                                problemDetails.Status = (int)HttpStatusCode.InternalServerError;
                                problemDetails.Title = "Database Operation Failed";
                                break;

                            default:
                                problemDetails.Status = (int)HttpStatusCode.InternalServerError;
                                problemDetails.Title = "Internal Server Error";
                                break;
                        }

                        problemDetails.Detail = exception.Message;
                        context.Response.StatusCode = problemDetails.Status.Value;
                        context.Response.ContentType = "application/problem+json";

                        await context.Response.WriteAsJsonAsync(problemDetails);
                    }
                });
            });
        }
    }
}