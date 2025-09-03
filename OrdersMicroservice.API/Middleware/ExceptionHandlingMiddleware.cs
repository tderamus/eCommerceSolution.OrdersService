namespace eCommerce.OrdersMicroservice.API.Middleware;

public class ExceptionHandlingMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<ExceptionHandlingMiddleware> _logger;

    public ExceptionHandlingMiddleware(RequestDelegate next, ILogger<ExceptionHandlingMiddleware> logger)
    {
        _next = next;
        _logger = logger;
    }

    public async Task Invoke(HttpContext httpContext)
    {

        try
        {
            // Call the next middleware in the pipeline
            await _next(httpContext);
        }
        catch (System.Exception ex)
        {
            // log the exception here
            _logger.LogError($"{ex.GetType().ToString()}: {ex.Message}");

            if (ex.InnerException != null)
            {
                _logger.LogError($"{ex.InnerException.ToString()}: {ex.InnerException.Message}");
            }

            // Set the response status code and content type
            httpContext.Response.StatusCode = StatusCodes.Status500InternalServerError;
            await httpContext.Response.WriteAsJsonAsync(new
            {
                StatusCode = httpContext.Response.StatusCode,
                Message = ex.Message,
                Type = ex.GetType().ToString(),
            });
        }
    }
}
// Extension method used to add the middleware to the HTTP request pipeline.
public static class ExceptionHandlingMiddlewareExtensions
{
    public static IApplicationBuilder UseExceptionHandlingMiddleware(this IApplicationBuilder builder)
    {
        return builder.UseMiddleware<ExceptionHandlingMiddleware>();
    }
}