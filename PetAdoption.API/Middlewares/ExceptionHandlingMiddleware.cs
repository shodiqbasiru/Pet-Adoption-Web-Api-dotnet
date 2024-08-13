using System.Security.Authentication;
using PetAdoption.Core.Exceptions;
using PetAdoption.Core.Models.Responses;

namespace PetAdoption.API.Middlewares;

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

            // var token = context.Request.Headers["Authorization"].ToString();
            // if (token.StartsWith("Bearer "))
            // {
            //     token = token[7..];
            // }

            // _logger.LogInformation("Token: {}", token);

            var path = context.Request.Path.Value!.ToLower();
            if (allowPaths.Contains(path))
            {
                await next(context);
                return;
            }

            if (!context.User.Identity.IsAuthenticated)
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
            NotFoundException => StatusCodes.Status404NotFound,
            UnauthorizedException => StatusCodes.Status401Unauthorized,
            AuthenticationException => StatusCodes.Status401Unauthorized,
            AuthorizationException => StatusCodes.Status403Forbidden,
            BadRequestException => StatusCodes.Status400BadRequest,
            DuplicateDataException => StatusCodes.Status409Conflict,
            _ => StatusCodes.Status500InternalServerError
        };

        context.Response.StatusCode = statusCode;
        var error = new ErrorResponse
        {
            StatusCode = statusCode,
            Message = exception.Message
        };


        await context.Response.WriteAsJsonAsync(error);
    }
}