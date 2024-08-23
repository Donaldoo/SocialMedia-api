using System.Net;
using FluentValidation;
using Newtonsoft.Json;
using SocialMedia.Application.Common.Exceptions;

namespace SocialMedia.Api.Common.MiddleWares;

public class ErrorHandlingMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<ErrorHandlingMiddleware> _logger;

    public ErrorHandlingMiddleware(RequestDelegate next, ILogger<ErrorHandlingMiddleware> logger)
    {
        _next = next;
        _logger = logger;
    }

    public async Task Invoke(HttpContext context, ILogger<ErrorHandlingMiddleware> logger)
    {
        try
        {
            await _next(context);
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "HandleExceptionAsync");
            await HandleExceptionAsync(context, ex);
        }
    }

    public class ErrorMessage
    {
        public int Code { get; }
        public string PropertyName { get; }
        public string Message { get; }

        public ErrorMessage(int code, string propertyName, string message)
        {
            Code = code;
            PropertyName = propertyName;
            Message = message;
        }
    }

    private async Task HandleExceptionAsync(HttpContext context, Exception exception)
    {
        _logger.LogError(exception, $"Generic exception: {exception}");

        var code = HttpStatusCode.InternalServerError;

        var result = new List<ErrorMessage>();

        switch (exception)
        {
            case ValidationException ex:
            {
                if (ex.Errors.Any())
                    result.AddRange(ex.Errors.Select(c => new ErrorMessage((int)code, c.PropertyName, c.ErrorMessage))
                        .ToList());
                else if (!string.IsNullOrWhiteSpace(ex.Message))
                    result.Add(new ErrorMessage((int)code, string.Empty, ex.Message));

                code = HttpStatusCode.BadRequest;
                break;
            }
            case EntityNotFoundException ex:
                code = HttpStatusCode.NotFound;
                result.Add(new ErrorMessage((int)code, ex.EntityName, ex.Message));
                break;
            case AuthorizationException ex:
                code = HttpStatusCode.Unauthorized;
                result.Add(new ErrorMessage(401, string.Empty, "Unauthorized."));
                break;
            default:
                result.Add(new ErrorMessage((int)code, string.Empty, "There has been an error."));
                break;
        }

        context.Response.ContentType = "application/json";
        context.Response.StatusCode = (int)code;

        await context.Response.WriteAsync(JsonConvert.SerializeObject(result));
    }
}

public static class ErrorHandlingMiddlewareExtensions
{
    public static IApplicationBuilder UseGenericErrorHandling(this IApplicationBuilder builder)
    {
        return builder.UseMiddleware<ErrorHandlingMiddleware>();
    }
}