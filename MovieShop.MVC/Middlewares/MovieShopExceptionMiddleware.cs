using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Net;
using System.Security.Claims;
using System.Threading.Tasks;
using ApplicationCore.Entities;
using ApplicationCore.Exceptions;
using ApplicationCore.Helpers;
using Microsoft.AspNetCore.Mvc.Diagnostics;
using Microsoft.Extensions.Logging;

namespace MovieShop.MVC.Middlewares
{
    // You may need to install the Microsoft.AspNetCore.Http.Abstractions package into your project
    public class MovieShopExceptionMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<MovieShopExceptionMiddleware> _logger;

        public MovieShopExceptionMiddleware(RequestDelegate next, ILogger<MovieShopExceptionMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task Invoke(HttpContext httpContext)
        {
            try
            {
                await _next(httpContext);
            }
            catch (Exception ex)
            {
                _logger.LogError("Middleware is catching exception");
                await HandleExceptionAsync(httpContext, ex);
            }

            //return _next(httpContext);
        }

        private async Task HandleExceptionAsync(HttpContext httpContext, Exception ex)
        {
            // get all the information you wanna log and use Serilog or NLog to log exceptions to text/json files
            _logger.LogError("Starting Logging for exception");

            ClaimsPrincipal principal = httpContext.User;

            var claimItems = new List<string>();
            foreach (Claim claim in principal.Claims)
            {
                claimItems.Add(claim.Value);
            }
            // claimItems = [FirstName, LastName, UserId, Email]

            var errorModel = new ErrorReponseModel
            {
                ExceptionMessage = ex.Message,
                ExceptionStackTree = ex.StackTrace,
                InnerExceptionMessage = ex.InnerException?.Message,
                FullName = claimItems[0] + " " + claimItems[1],
                UserID = claimItems[2],
                Email = claimItems[3],
                ExceptionDateTime = DateTime.UtcNow
            };

            switch (ex)
            {
                case ConflictException conflictException:
                    httpContext.Response.StatusCode = (int) HttpStatusCode.Conflict;
                    break;

                case NotFoundException notFoundException:
                    httpContext.Response.StatusCode = (int) HttpStatusCode.NotFound;
                    break;

                case UnauthorizedAccessException unauthorized:
                    httpContext.Response.StatusCode = (int) HttpStatusCode.Unauthorized;
                    break;

                case Exception exception:
                    httpContext.Response.StatusCode = (int) HttpStatusCode.InternalServerError;
                    break;
            }

            // seriLog to log errorModel along with 
            // send email also
            var message =
                $"Exception Code: {httpContext.Response.StatusCode}, {errorModel.ExceptionMessage}, {errorModel.ExceptionStackTree}, {errorModel.InnerExceptionMessage}, {errorModel.FullName}, {errorModel.UserID}, {errorModel.Email} at {errorModel.ExceptionDateTime.ToLongDateString()}";
            
            _logger.LogInformation(message);
            _logger.LogCritical(message);

            // redirect to error page
            httpContext.Response.Redirect("/Home/Error");
            await Task.CompletedTask;
        }
    }

    // Extension method used to add the middleware to the HTTP request pipeline.
    public static class MovieShopExceptionMiddlewareExtensions
    {
        public static IApplicationBuilder UseMovieShopExceptionMiddleware(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<MovieShopExceptionMiddleware>();
        }
    }
}
