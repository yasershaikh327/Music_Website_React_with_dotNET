using Microsoft.AspNetCore.Http;
using System;
using System.Threading.Tasks;

public class ErrorHandlingMiddleware
{
    private readonly RequestDelegate _next;

    public ErrorHandlingMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (Exception ex)
        {
            context.Response.Redirect($"/Error/GetError?errorMessage={Uri.EscapeDataString(ex.Message)}");
        }
    }
}
