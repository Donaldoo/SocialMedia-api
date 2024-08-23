using System.Diagnostics;
using MediatR;
using Microsoft.Extensions.Logging;

namespace SocialMedia.Application.Common.Behaviours;

public class RequestPerformanceBehaviour<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
{
    private readonly Stopwatch _timer;
    private readonly ILogger<TRequest> _logger;
    private readonly ICurrentUser _currentUser;

    public RequestPerformanceBehaviour(ILogger<TRequest> logger, ICurrentUser currentUser)
    {
        _timer = new Stopwatch();
        _logger = logger;
        _currentUser = currentUser;
    }

    public async Task<TResponse> Handle(TRequest request,
        RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
    {
        _timer.Start();

        var response = await next();

        _timer.Stop();

        if (_timer.ElapsedMilliseconds > 500)
        {
            var name = typeof(TRequest).Name;

            if (_currentUser != null)
            {
                _logger.LogWarning(
                    "Pk-App Long Running Request: {Name} ({ElapsedMilliseconds} milliseconds) {@UserId} {@Request}",
                    name, _timer.ElapsedMilliseconds, _currentUser.UserId, request);
            }
            else
            {
                _logger.LogWarning(
                    "Pk-App Long Running Request: {Name} ({ElapsedMilliseconds} milliseconds) {@Request}",
                    name, _timer.ElapsedMilliseconds, request);
            }
        }

        return response;
    }
}