using System;
using ApplicationCore.ServiceInterfaces;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Logging;

namespace Infrastructure.Filters
{
    public class MovieShopHeaderFilter:IActionFilter
    {
        private readonly ICurrentUserService _currentUserService;
        private readonly ILogger<MovieShopHeaderFilter> _logger;

        public MovieShopHeaderFilter(ICurrentUserService currentUserService, ILogger<MovieShopHeaderFilter> logger)
        {
            _currentUserService = currentUserService;
            _logger = logger;
        }

        public void OnActionExecuting(ActionExecutingContext context)
        {
            //Log each and every user's IP address, his name if authenticated, authentication status, datatime
            //context.HttpContext.Response.Headers.Add("job","antra.com/jobs");

            var email = _currentUserService.Email;
            var datetime = DateTime.UtcNow.ToLongTimeString();
            var isAuthenticated = _currentUserService.IsAuthenticated.ToString();
            var name = _currentUserService.FullName;

            // log this into text files
            // System.IO
            // Serilog, Nlog, Log4net
            var message = $"{_currentUserService.RemoteIpAddress} {email}, {name} visited at {datetime}, and {name} is {isAuthenticated} authenticated";
            _logger.LogInformation(message);
            _logger.LogCritical(message);
        }

        public void OnActionExecuted(ActionExecutedContext context)
        {
            //int x = 20 / 10;
        }
    }
}