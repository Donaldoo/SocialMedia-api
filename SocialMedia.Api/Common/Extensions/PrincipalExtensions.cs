using System.Security.Claims;
using System.Security.Principal;
using Newtonsoft.Json;
using SocialMedia.Application.Account.Authenticator;

namespace SocialMedia.Api.Common.Extensions;

public static class PrincipalExtensions
{
    public static AuthenticationInfoResponse GetUserAuthenticationInfo(this IPrincipal currentPrincipal)
    {
        var identity = currentPrincipal.Identity as ClaimsIdentity;
        var claim = identity?.Claims.FirstOrDefault(c => c.Type == ClaimTypes.UserData);
        return claim != null ? JsonConvert.DeserializeObject<AuthenticationInfoResponse>(claim.Value) : null;
    }
}