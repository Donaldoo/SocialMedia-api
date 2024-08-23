using MediatR;
using SocialMedia.Application.Common.Exceptions;
using SocialMedia.Application.Common.Requests;

namespace SocialMedia.Application.Common.Behaviours;

public class AuthenticateBehaviour<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
{
    private readonly ICurrentUser _currentUser;

    public AuthenticateBehaviour(ICurrentUser currentUser)
    {
        _currentUser = currentUser;
    }


    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next,
        CancellationToken cancellationToken)
    {
        if (!(request is IAuthenticatedRequest))
            return await next();

        if (_currentUser == null)
            throw new AuthorizationException();

        return await next();
    }
}