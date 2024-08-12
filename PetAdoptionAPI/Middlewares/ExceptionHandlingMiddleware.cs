using System.Net;
using System.Security.Authentication;
using PetAdoptionAPI.Exceptions;
using PetAdoptionAPI.Models.Responses;

namespace PetAdoptionAPI.Middlewares;

public class ExceptionHandlingMiddleware : IMiddleware
{
    private readonly ILogger<ExceptionHandlingMiddleware> _logger;

    public ExceptionHandlingMiddleware(ILogger<ExceptionHandlingMiddleware> logger)
    {
        _logger = logger;
    }


    public async Task InvokeAsync(HttpContext context, RequestDelegate next)
    {
        try
        {
            var allowPaths = new List<string>
            {
                "/api/auth/login",
                "/api/auth/register",
                "/api/auth/register-seller"
            };

            var path = context.Request.Path.Value!.ToLower();
            if (allowPaths.Contains(path))
            {
                await next(context);
                return;
            }

            if(!context.User.Identity!.IsAuthenticated)
            {
                throw new AuthenticationException("User is not authenticated");
            }
            await next(context);
    
        }
        catch (Exception e)
        {
            _logger.LogError("Error occurred: {}", e);
            
            await HandlingExceptionAsync(context, e);
        }
    }

    private static async Task HandlingExceptionAsync(HttpContext context, Exception exception)
    {
        context.Response.ContentType = "application/json";
        var statusCode = exception switch
        {
            NotFoundException => (int)HttpStatusCode.NotFound,
            UnauthorizedException => (int)HttpStatusCode.Unauthorized,
            AuthenticationException => (int)HttpStatusCode.Unauthorized,
            AuthorizationException => (int)HttpStatusCode.Forbidden,
            BadRequestException => (int)HttpStatusCode.BadRequest,
            DuplicateDataException => (int)HttpStatusCode.Conflict,
            _ => (int)HttpStatusCode.InternalServerError
        };

        context.Response.StatusCode = statusCode;
        var error = new ErrorResponse{
            StatusCode = statusCode,
            Message = exception.Message
        };
       

        await context.Response.WriteAsJsonAsync(error);
    }
}