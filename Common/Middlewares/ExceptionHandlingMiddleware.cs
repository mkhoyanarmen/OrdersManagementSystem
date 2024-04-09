using Common.Exceptions;
using Google.Protobuf.WellKnownTypes;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Common.Middlewares
{
    public class ExceptionHandlingMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ExceptionHandlingMiddleware> _logger;

        public ExceptionHandlingMiddleware(
            RequestDelegate next,
            ILogger<ExceptionHandlingMiddleware> logger)
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
            catch (Exception exception)
            {
                _logger.LogError(
                    exception, "Exception occurred: {Message}", exception.Message);

                var problemDetails = new ProblemDetails
                {
                    Status = StatusCodes.Status500InternalServerError,
                    Title = "Server Error"
                };

                switch (exception)
                {
                    case NotFoundException e:
                        problemDetails.Title = e.Message;
                        problemDetails.Status = StatusCodes.Status404NotFound;
                        break;
                }

                context.Response.StatusCode = problemDetails.Status.Value;
                await context.Response.WriteAsJsonAsync(problemDetails);
            }
        }
    }

    public static class ExceptionHandlingMiddlewareExtensions
    {
        public static void UseExceptionHandling(this WebApplication app)
        {
            app.UseMiddleware<ExceptionHandlingMiddleware>();
        }
    }
}
