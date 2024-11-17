using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using System.Security.Claims;

namespace BaseballManagerApp.Client.Identity;

public class ClientAuthenticationStateProvider : AuthenticationStateProvider
{
    private readonly Task<AuthenticationState> _authenticationState;

    public ClientAuthenticationStateProvider(PersistentComponentState componentState)
    {
        var authenticationState = InitializeAuthenticationState(componentState);
        _authenticationState = Task.FromResult(authenticationState);
    }

    public override Task<AuthenticationState> GetAuthenticationStateAsync() => _authenticationState;

    private AuthenticationState InitializeAuthenticationState(PersistentComponentState componentState)
    {
        var claimsPrincipal = CreateClaimsPrincipal(componentState);
        return new AuthenticationState(claimsPrincipal);
    }

    private ClaimsPrincipal CreateClaimsPrincipal(PersistentComponentState componentState)
    {
        if (componentState.TryTakeFromJson<UserModel>(nameof(UserModel), out var userModel) && userModel != null)
        {
            List<Claim> claims =
            [
                new Claim(ClaimTypes.Name, userModel.Name ?? string.Empty),
                new Claim(ClaimTypes.Email, userModel.Email ?? string.Empty),
                new Claim(ClaimTypes.NameIdentifier, userModel.UserId)
            ];

            var claimsIdentity = new ClaimsIdentity(claims, "BaseballAuthScheme");
            return new ClaimsPrincipal(claimsIdentity);
        }
        
        return new ClaimsPrincipal(new ClaimsIdentity());
    }
}
