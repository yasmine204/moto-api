using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using MotoRentAPI.Dtos;

public class ValidationExceptionFilter : IExceptionFilter, IActionFilter
{
    public void OnActionExecuting(ActionExecutingContext context)
    {
        if (!context.ModelState.IsValid)
        {
            context.Result = new BadRequestObjectResult(
                new MessageResponseDto { Message = "Invalid data" }
            );
        }
    }

    public void OnActionExecuted(ActionExecutedContext context) { }

    public void OnException(ExceptionContext context)
    {
        if (context.Exception is ArgumentException)
        {
            context.Result = new BadRequestObjectResult(
                new MessageResponseDto { Message = context.Exception.Message }
            );

            context.ExceptionHandled = true;
        }
    }
}
