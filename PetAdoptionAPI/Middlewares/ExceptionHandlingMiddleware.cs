using System.Net;
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