using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using System.Security.Claims;
using PersonalUrlShortener.Shared.AuthenticationStateSyncer;

namespace PersonalUrlShortener.Client.AuthenticationStateSyncer;

public class PersistentAuthenticationStateProvider(PersistentComponentState persistentState) : AuthenticationStateProvider
{
    private static readonly Task<AuthenticationState> UnauthenticatedTask =
        Task.FromResult(new AuthenticationState(new ClaimsPrincipal(new ClaimsIdentity())));

    public override Task<AuthenticationState> GetAuthenticationStateAsync()
    {
        if (!persistentState.TryTakeFromJson<UserInfo>(nameof(UserInfo), out var userInfo) || userInfo is null)
        {
            return UnauthenticatedTask;
        }

        Claim[] claims = [
            new(ClaimTypes.NameIdentifier, userInfo.UserId),
            new(ClaimTypes.Name, userInfo.Name ?? string.Empty),
            new(ClaimTypes.Email, userInfo.Email ?? string.Empty)];

        return Task.FromResult(
            new AuthenticationState(new ClaimsPrincipal(new ClaimsIdentity(claims,
                authenticationType: nameof(PersistentAuthenticationStateProvider)))));
    }
}