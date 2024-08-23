namespace SocialMedia.Api.Common.MiddleWares;

public class PreventClickJackingMiddleware
{
    private readonly RequestDelegate _next;

    public PreventClickJackingMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        context.Response.Headers.Add("X-Frame-Options", "DENY");
        await _next(context);
    }
}

public static class PreventClickJackingMiddlewareExtensions
{
    public static IApplicationBuilder UsePreventClickJacking(this IApplicationBuilder builder)
    {
        return builder.UseMiddleware<PreventClickJackingMiddleware>();
    }
}