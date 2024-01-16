
using System.Net;
using System.Text.Json;
using Microsoft.AspNetCore.Mvc;

namespace AuthApp.Middlewares;

public class MiddlewareMethodThree : IMiddleware
{

    private readonly ILogger _logger;

    public MiddlewareMethodThree(ILogger<MiddlewareMethodThree> logger)
    {
        _logger = logger;
    }

    public async Task InvokeAsync(HttpContext context, RequestDelegate next)
    {
        try
        {
           Console.WriteLine("looooooooooooog 333333333");
            await next(context);
        }
        catch (Exception e)
        {
           _logger.LogError(">>>>>>>>>>>>>>>>>>>>>>>>>>>>");
            ProblemDetails problem=new ()
            {
                Status=(int)HttpStatusCode.InternalServerError,
                Type="Server errror",
                Title="Server error Titlte",
                Detail="An internal server has occurred"
            };
            string json=JsonSerializer.Serialize(problem);
            context.Response.ContentType="application/json";
            context.Response.StatusCode=(int)HttpStatusCode.InternalServerError;
            //*below code must be at the end 
            await context.Response.WriteAsync(json);
        }
    }
}