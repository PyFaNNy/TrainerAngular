using System.Net;

namespace Trainer.Middlewares;

public class ErrorHandlerMiddleware
{
    private readonly RequestDelegate _next;

    public ErrorHandlerMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task Invoke(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (Exception error)
        {
            Console.WriteLine(error.Message);
            var response = context.Response;
            response.ContentType = "application/json";

            switch (error)
            {
                default:
                    // unhandled error
                    response.StatusCode = (int) HttpStatusCode.InternalServerError;
                    await response.WriteAsync("An error occurred while processing your request.");
                    break;
            }
            
        }
    }
}