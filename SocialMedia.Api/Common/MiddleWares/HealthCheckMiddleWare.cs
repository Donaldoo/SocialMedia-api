namespace SocialMedia.Api.Common.MiddleWares;

public class HealthCheckMiddleWare
{
    private readonly RequestDelegate _next;

    public HealthCheckMiddleWare(RequestDelegate next)
    {
        _next = next;
    }
        
    public async Task InvokeAsync(HttpContext context)
    {
        if (context.Request.Path.Value.Equals("/healthcheck"))
            await context.Response.WriteAsync("Ok");
        else
            await _next(context);
    }
}
    
public static class HealthCheckMiddleWareExtensions
{
    public static IApplicationBuilder UseAppHealthCheck(this IApplicationBuilder builder)
    {
        return builder.UseMiddleware<HealthCheckMiddleWare>();
    }
}