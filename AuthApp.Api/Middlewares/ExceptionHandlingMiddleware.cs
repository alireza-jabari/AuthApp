using System.Net;

namespace AuthApp.Middlewares;

// secondMethodMiddleware
public class ExceptionHandlingMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger _logger;

    public ExceptionHandlingMiddleware(RequestDelegate next,ILogger<ExceptionHandlingMiddleware> logger)
    {
        _next = next;
        _logger=logger;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            Console.WriteLine("hhhhhhhhhhhhhhhhhhhhhhhhhhhhh");
            await _next(context);
        }
        catch (Exception e)
        {   
            context.Response.StatusCode=(int)HttpStatusCode.InternalServerError;
            Console.WriteLine("++++++++++++++++22222222222222222+++++++++++++++");
            // _logger.LogError(e,e.Message);
            // _logger.LogError("++++++++++++++++22222222222222222+++++++++++++++");
        }
    }
}