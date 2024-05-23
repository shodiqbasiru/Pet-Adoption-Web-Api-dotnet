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
            var path = context.Request.Path;
            if (path.StartsWithSegments("/api/auth/login") || path.StartsWithSegments("/api/auth/register"))
            {
                await next(context);
                return;
            }
            
            var authHeader = context.Request.Headers["Authorization"].FirstOrDefault();
            if (authHeader == null)
            {
                throw new UnauthorizedException("Full authentication is required to access this resource");
            }
            await next(context);
        }
        catch (Exception e)
        {
            await HandlingExceptionAsync(context, e);
        }
    }

    private static async Task HandlingExceptionAsync(HttpContext context, Exception exception)
    {
        var error = new ErrorResponse();
        switch (exception)
        {
            case NotFoundException:
                error.Code = (int)HttpStatusCode.NotFound;
                error.Status = HttpStatusCode.NotFound.ToString();
                error.Message = exception.Message;
                context.Response.StatusCode = (int)HttpStatusCode.NotFound;
                break;
            case UnauthorizedException:
                error.Code = (int)HttpStatusCode.Unauthorized;
                error.Status = HttpStatusCode.Unauthorized.ToString();
                error.Message = exception.Message;
                context.Response.StatusCode = (int)HttpStatusCode.Unauthorized;
                break;
            case AuthenticationException:
                error.Code = (int)HttpStatusCode.Unauthorized;
                error.Status = HttpStatusCode.Unauthorized.ToString();
                error.Message = exception.Message;
                context.Response.StatusCode = (int)HttpStatusCode.Unauthorized;
                break;
            case AuthorizationException:
                error.Code = (int)HttpStatusCode.Forbidden;
                error.Status = HttpStatusCode.Forbidden.ToString();
                error.Message = exception.Message;
                context.Response.StatusCode = (int)HttpStatusCode.Forbidden;
                break;
            case BadRequestException:
                error.Code = (int)HttpStatusCode.BadRequest;
                error.Status = HttpStatusCode.BadRequest.ToString();
                error.Message = exception.Message;
                context.Response.StatusCode = (int)HttpStatusCode.BadRequest;
                break;
            case DuplicateDataException:
                error.Code = (int)HttpStatusCode.Conflict;
                error.Status = HttpStatusCode.Conflict.ToString();
                error.Message = exception.Message;
                context.Response.StatusCode = (int)HttpStatusCode.Conflict;
                break;
            case not null:
                break;
        }

        await context.Response.WriteAsJsonAsync(error);
    }
}