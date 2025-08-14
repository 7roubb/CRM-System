<<<<<<< HEAD
﻿// ExceptionHandler.cs
using CRM.Exceptions;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using System.Net;
=======
﻿using CRM.Exceptions;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
>>>>>>> main

namespace CRM.Middleware
{
    public static class ExceptionHandler
    {
<<<<<<< HEAD
        public static void UseCustomExceptionHandler(this IApplicationBuilder app)
=======
        public static void UseCustomExceptionHandler(this IApplicationBuilder app, IWebHostEnvironment env)
>>>>>>> main
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
<<<<<<< HEAD
=======
                            case NoteNotFoundException:
>>>>>>> main
                                problemDetails.Status = (int)HttpStatusCode.NotFound;
                                problemDetails.Title = "Resource Not Found";
                                logger.LogWarning(exception, "Resource not found: {Message}", exception.Message);
                                break;

                            case UserAlreadyExistsException:
<<<<<<< HEAD
                            case ContactAlreadyExistsException: 
=======
                            case ContactAlreadyExistsException:
                            case NotesAlreadyExistsException:
>>>>>>> main
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
<<<<<<< HEAD
                          
=======
>>>>>>> main
                            default:
                                problemDetails.Status = (int)HttpStatusCode.InternalServerError;
                                problemDetails.Title = "Internal Server Error";
                                logger.LogError(exception, "Unhandled exception: {Message}", exception.Message);
                                break;
                        }

                        problemDetails.Detail = exception.Message;

                        context.Response.StatusCode = problemDetails.Status.Value;
                        context.Response.ContentType = "application/problem+json";

<<<<<<< HEAD
                        var env = context.RequestServices.GetRequiredService<IWebHostEnvironment>();
=======
>>>>>>> main
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
<<<<<<< HEAD
}
=======
}
>>>>>>> main
