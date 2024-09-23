using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace ReactApp.Server.ExceptionFilter
{
    public class MyExceptionFilter : ExceptionFilterAttribute
    {
        public override void OnException(ExceptionContext context)
        {
            var ex = context.Exception;

            // Redirect to the Error action and pass the error message
            context.Result = new RedirectToActionResult("Index", "Error", new { errorMessage = ex.Message });

            // Mark exception as handled
            context.ExceptionHandled = true;
        }
    }

}
