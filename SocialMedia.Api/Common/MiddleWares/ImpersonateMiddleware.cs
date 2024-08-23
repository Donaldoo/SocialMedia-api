using MediatR;

namespace SocialMedia.Api.Common.MiddleWares;

public class ImpersonateMiddleware
{
    private readonly RequestDelegate _next;

    public ImpersonateMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task InvokeAsync(HttpContext context, IMediator mediator)
    {
        await _next(context);
    }
}


public static class ImpersonateMiddlewareExtensions
{
    public static IApplicationBuilder UseImpersonation(this IApplicationBuilder builder)
    {
        return builder.UseMiddleware<ImpersonateMiddleware>();
    }
}