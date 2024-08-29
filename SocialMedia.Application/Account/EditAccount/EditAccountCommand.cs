using MediatR;
using SocialMedia.Application.Common.Requests;

namespace SocialMedia.Application.Account.EditAccount;

public record EditAccountCommand : IRequest<Guid>, IAuthenticatedRequest
{
    public string FirstName { get; init; }
    public string LastName { get; init; }
    public string Email { get; init; }
    public string Password { get; init; }
    public string City { get; init; }
    public string Website { get; init; }
    public string ProfilePicture { get; init; }
    public string CoverPicture { get; init; }
};