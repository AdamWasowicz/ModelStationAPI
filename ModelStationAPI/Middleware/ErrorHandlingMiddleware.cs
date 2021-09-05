using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using ModelStationAPI.Exceptions;

namespace ModelStationAPI.Middleware
{
    public class ErrorHandlingMiddleware : IMiddleware
    {
        private readonly ILogger<ErrorHandlingMiddleware> _logger;

        public ErrorHandlingMiddleware(ILogger<ErrorHandlingMiddleware> logger)
        {
            _logger = logger;
        }

        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            try
            {
                await next.Invoke(context);
            }
            catch (BadRequestException exception)
            {
                _logger.LogError(exception, exception.Message);

                context.Response.StatusCode = 400;
                await context.Response.WriteAsync(exception.Message);
            }
            catch (NoPermissionException exception)
            {
                _logger.LogError(exception, exception.Message);

                context.Response.StatusCode = 403;
                await context.Response.WriteAsync(exception.Message);
            }
            catch (NotFoundException notFoundException)
            {
                _logger.LogError(notFoundException, notFoundException.Message);

                context.Response.StatusCode = 404;
                await context.Response.WriteAsync(notFoundException.Message);
            }
            catch (UserBannedException exception)
            {
                _logger.LogError(exception, exception.Message);

                context.Response.StatusCode = 403;
                await context.Response.WriteAsync(exception.Message);
            }
            catch (UnauthorizedAccessException exception)
            {
                _logger.LogError(exception, exception.Message);

                context.Response.StatusCode = 401;
                await context.Response.WriteAsync(exception.Message);
            }
            catch (Exception e)
            {
                _logger.LogError(e, e.Message);

                context.Response.StatusCode = 500;
                await context.Response.WriteAsync("Undocumented Error");
            }
        }
    }
}
