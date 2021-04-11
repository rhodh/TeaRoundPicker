using Application.HttpExceptions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;


namespace WebAPI.Filters
{
    public class HttpResponseExceptionsFilter : ExceptionFilterAttribute
    {
        public override void OnException(ExceptionContext context)
        {
            if (context.Exception is HttpResponseException exception)
            {
                context.Result = new ObjectResult(exception.ProblemDetails)
                {
                    ContentTypes = { "application/problem+json" },
                    StatusCode = exception.ProblemDetails.Status
                };
                context.ExceptionHandled = true;
            }
        }
    }
}
