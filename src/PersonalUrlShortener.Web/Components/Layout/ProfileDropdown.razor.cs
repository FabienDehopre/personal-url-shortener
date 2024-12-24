using System.Diagnostics.CodeAnalysis;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;

namespace PersonalUrlShortener.Web.Components.Layout;

public partial class ProfileDropdown : ComponentBase
{
    [Inject] private AuthenticationStateProvider AuthenticationStateProvider { get; set; } = default!;
    private bool UserMenuVisible { get; set; } = false;
    private string? UserName { get; set; }
    private string? Picture { get; set; }
    
    protected override async Task OnInitializedAsync()
    {
        var authState = await AuthenticationStateProvider.GetAuthenticationStateAsync();
        var user = authState.User;
        if (user.Identity is { IsAuthenticated: true })
        {
            UserName = user.FindFirst("nickname")?.Value;
            Picture = user.FindFirst("picture")?.Value;
        }
    }
    
    private void ToggleUserMenu() => UserMenuVisible = !UserMenuVisible;

    private void CloseUserMenu()
    {
        UserMenuVisible = false;
        StateHasChanged();
    }
}