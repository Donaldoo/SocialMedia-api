using MediatR;
using SocialMedia.Application.Common;
using SocialMedia.Application.Common.Data;
using SocialMedia.Domain.Account;

namespace SocialMedia.Application.Account.EditAccount;

internal sealed class EditAccountCommandHandler : IRequestHandler<EditAccountCommand,Guid>
{
    private readonly IDataWriter _dataWriter;
    private readonly ICurrentUser _currentUser;
    private readonly ISettings _settings;
    private readonly IPasswordHasher _passwordHasher;

    public EditAccountCommandHandler(IDataWriter dataWriter, ICurrentUser currentUser, ISettings settings, IPasswordHasher passwordHasher)
    {
        _dataWriter = dataWriter;
        _currentUser = currentUser;
        _settings = settings;
        _passwordHasher = passwordHasher;
    }

    public async Task<Guid> Handle(EditAccountCommand request, CancellationToken cancellationToken)
    {
        await _dataWriter.UpdateOneAsync<User>(u => u with
        {
            FirstName = request.FirstName,
            LastName = request.LastName,
            Email = request.Email,
            Password = string.IsNullOrWhiteSpace(request.Password) ? u.Password : _passwordHasher.HashPassword(request.Password),
            City = request.City,
            Bio = request.Bio,
            ProfilePicture = request.ProfilePicture == u.ProfilePicture ? u.ProfilePicture : _settings.WebApiUrl + request.ProfilePicture,
            CoverPicture = request.CoverPicture == u.CoverPicture ? u.CoverPicture : _settings.WebApiUrl + request.CoverPicture,
        }, _currentUser.UserId);
        await _dataWriter.SaveAsync(cancellationToken);
        return _currentUser.UserId;
    }
}