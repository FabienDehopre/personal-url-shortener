using Auth0.AspNetCore.Authentication;
using Blazorise;
using Blazorise.Icons.FontAwesome;
using IdGen.DependencyInjection;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using PersonalUrlShortener.Components;
using PersonalUrlShortener.Infrastructure;
using PersonalUrlShortener.Infrastructure.Data;

var builder = WebApplication.CreateBuilder(args);

builder.AddServiceDefaults();
builder.AddNpgsqlDbContext<AppDbContext>("personal-url-shortener-db");
builder.AddRedisOutputCache("cache");
// https://learn.microsoft.com/en-us/dotnet/aspire/caching/caching-integrations?tabs=dotnet-cli#configure-the-api-with-distributed-caching
builder.Services.AddIdGen(1); // TODO: find a way to generate the generator id

builder.Services.AddAuth0WebAppAuthentication(options =>
{
    options.Domain = builder.Configuration["Auth0:Domain"] ?? throw new ApplicationException("Missing Auth0 Domain");
    options.ClientId = builder.Configuration["Auth0:ClientId"] ?? throw new ApplicationException("Missing Auth0 ClientId");
});
builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("OnlyMe", policy => 
        policy.RequireAuthenticatedUser()
            .RequireUserName("fabien@dehopre.com"));
});

builder.Services.AddCascadingAuthenticationState();

builder.Services.AddBlazorise().AddFontAwesomeIcons().AddEmptyProviders();

// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents()
    .AddInteractiveWebAssemblyComponents();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseWebAssemblyDebugging();
}
else
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseStaticFiles();
app.UseAntiforgery();

app.MapGet("/Account/Login", async (HttpContext httpContext, string returnUrl = "/") =>
{
    var authenticationProperties = new LoginAuthenticationPropertiesBuilder()
        .WithRedirectUri(returnUrl)
        .Build();

    await httpContext.ChallengeAsync(Auth0Constants.AuthenticationScheme, authenticationProperties);
});

app.MapGet("/Account/Logout", async (HttpContext httpContext) =>
{
    var authenticationProperties = new LogoutAuthenticationPropertiesBuilder()
        .WithRedirectUri("/")
        .Build();

    await httpContext.SignOutAsync(Auth0Constants.AuthenticationScheme, authenticationProperties);
    await httpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
});

app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode()
    .AddInteractiveWebAssemblyRenderMode();

app.MapFallback(() =>
{
    return Results.Redirect("/weather");
});

app.Run();