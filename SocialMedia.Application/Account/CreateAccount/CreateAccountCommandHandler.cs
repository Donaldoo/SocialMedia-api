using MediatR;
using SocialMedia.Application.Account.Authenticator;
using SocialMedia.Application.Common.Data;
using SocialMedia.Domain.Account;

namespace SocialMedia.Application.Account.CreateAccount;

internal class CreateAccountCommandHandler : IRequestHandler<CreateAccountCommand, AuthenticationInfoResponse>
{
    private readonly IPasswordHasher _passwordHasher;
    private readonly IDataWriter _dataWriter;
    private readonly IMediator _mediator;

    public CreateAccountCommandHandler(IPasswordHasher passwordHasher, IDataWriter dataWriter, IMediator mediator)
    {
        _passwordHasher = passwordHasher;
        _dataWriter = dataWriter;
        _mediator = mediator;
    }

    public async Task<AuthenticationInfoResponse> Handle(CreateAccountCommand request,
        CancellationToken cancellationToken)
    {
        var account = new User
        {
            FirstName = request.FirstName,
            LastName = request.LastName,
            Email = request.Email,
            Password = _passwordHasher.HashPassword(request.Password),
            City = request.City,
            Website = request.Website
        };
        await _dataWriter.Add(account).SaveAsync(cancellationToken);

        return await _mediator.Send(new GetAuthenticationInfoQuery(account.Id), cancellationToken);
    }
}