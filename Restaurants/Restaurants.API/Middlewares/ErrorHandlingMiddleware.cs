
using Microsoft.AspNetCore.Mvc;
using Restaurants.Domain.Exceptions;
using System.Net;

namespace Restaurants.API.Middlewares;

public class ErrorHandlingMiddleware(ILogger<ErrorHandlingMiddleware> logger) : IMiddleware
{
    public async Task InvokeAsync(HttpContext context, RequestDelegate next)
    {
        try
        {
            await next.Invoke(context);
        }
        catch (NotFoundException ex) 
        {
            context.Response.StatusCode = 404;
            await context.Response.WriteAsync(ex.Message);

            logger.LogWarning(ex.Message);

        }
        catch (Exception ex)
        {
            context.Response.StatusCode = 500;
            await context.Response.WriteAsync(ex.Message);

            logger.LogWarning(ex.Message);

        }
    }
}
