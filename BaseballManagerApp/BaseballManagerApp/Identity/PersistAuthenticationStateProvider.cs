using BaseballManagerApp.Client.Identity;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.Server;
using Microsoft.AspNetCore.Components.Web;
using System.Security.Claims;

namespace BaseballManagerApp.Identity;

public class PersistAuthenticationStateProvider : ServerAuthenticationStateProvider, IDisposable
{
    private readonly PersistentComponentState _componentState;
    private readonly PersistingComponentStateSubscription _subscription;
    private Task<AuthenticationState>? _authenticationState;

    public PersistAuthenticationStateProvider(PersistentComponentState persistentComponentState)
    {
        _componentState = persistentComponentState;

        AuthenticationStateChanged += OnAuthenticationStateChanged;
        _subscription = _componentState.RegisterOnPersisting(OnPersistingAsync, RenderMode.InteractiveWebAssembly);
    }

    private void OnAuthenticationStateChanged(Task<AuthenticationState> task) => _authenticationState = task;

    private async Task OnPersistingAsync()
    {
        if (_authenticationState is null) throw new InvalidOperationException("Error with authentication state");

        var authenticationState = await _authenticationState;
        var claimsPrincipal = authenticationState.User;

        if (claimsPrincipal.Identity!.IsAuthenticated)
        {
            var userId = claimsPrincipal.Claims.FirstOrDefault(o => o.Type == ClaimTypes.NameIdentifier)?.Value;
            var email = claimsPrincipal.Claims.FirstOrDefault(o => o.Type == ClaimTypes.Email)?.Value;
            var name = claimsPrincipal.Claims.FirstOrDefault(x => x.Type == ClaimTypes.Name)!.Value;

            _componentState.PersistAsJson(nameof(UserModel), new UserModel
            {
                UserId = userId ?? "",
                Email = email,
                Name = name!
            });
        }
    }

    public void Dispose()
    {
        _subscription.Dispose();
        AuthenticationStateChanged -= OnAuthenticationStateChanged;
    }
}
