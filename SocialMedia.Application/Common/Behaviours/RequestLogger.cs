using MediatR.Pipeline;
using Microsoft.Extensions.Logging;

namespace SocialMedia.Application.Common.Behaviours;

public class RequestLogger<TRequest> : IRequestPreProcessor<TRequest>
{
    private readonly ILogger _logger;
    private readonly ICurrentUser _currentUser;

    public RequestLogger(ILogger<TRequest> logger, ICurrentUser currentUser)
    {
        _logger = logger;
        _currentUser = currentUser;
    }

    public Task Process(TRequest request, CancellationToken cancellationToken)
    {
        var name = typeof(TRequest).Name;
        
        if (_currentUser != null)
        {
            _logger.LogInformation("Pk-App Request: {Name} {@UserId} {@Request}",
                name, _currentUser.UserId, request);
        }
        else
        {
            _logger.LogInformation("Pk-App Request: {Name} {@Request}",
                name, request);
        }


        return Task.CompletedTask;
    }
}