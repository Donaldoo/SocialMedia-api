using FluentValidation;
using MediatR;

namespace SocialMedia.Application.Account.Authenticator;

internal class AuthenticationCommandHandler : IRequestHandler<AuthenticationCommand, AuthenticationCommandResponse>
{
    private readonly IMediator _mediator;
    private readonly IPasswordHasher _passwordHasher;

    public AuthenticationCommandHandler(
        IPasswordHasher passwordHasher,
        IMediator mediator)
    {
        _passwordHasher = passwordHasher;
        _mediator = mediator;
    }

    public async Task<AuthenticationCommandResponse> Handle(AuthenticationCommand request,
        CancellationToken cancellationToken)
    {
        var user = await _mediator.Send(new GetUserByEmailQuery
        {
            Email = request.Email
        }, cancellationToken);
        
        var passwordMatch = false;
        passwordMatch = _passwordHasher.VerifyHashedPassword(user.Password, request.Password);
        if (!passwordMatch)
            throw new ValidationException("Failed to authenticate, Password is invalid");

        var data = await _mediator.Send(new GetAuthenticationInfoQuery(user.Id), cancellationToken);

        return new AuthenticationCommandResponse
        {
            Data = data,
            Status = AuthenticationCommandResponse.AuthenticationStatus.Ok
        };
    }
}