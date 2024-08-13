using System;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using PetAdoption.Models.Responses;

namespace PetAdoption.filters;

public class ValidateEntityFilter : ActionFilterAttribute
{
    public override void OnActionExecuting(ActionExecutingContext context)
    {
        if (!context.ModelState.IsValid)
        {
            var errorResponse = new ErrorResponse(){
                StatusCode = StatusCodes.Status400BadRequest,
                Message = "The request is invalid.",
                Errors = context.ModelState.Values.SelectMany(err => err.Errors.Select(e => e.ErrorMessage)).ToList()
            };

            context.Result = new BadRequestObjectResult(errorResponse);
        }
    }
}
