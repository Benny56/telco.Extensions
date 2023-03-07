using Microsoft.AspNetCore.Components.WebAssembly.Authentication;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;


namespace telco.Extensions.DependencyInjection;

public static class WebAssemblyHostBuilderExtension
{
    /// <summary>
    /// Use the authentication provider Azure Ad B2C with msal.js to obtain an access tokens and register a <see cref="HttpClient"/> for API requests.
    /// You need to provide the Azure Ad B2C and your API settings via appsettings.json.
    /// </summary>
    public static IRemoteAuthenticationBuilder<RemoteAuthenticationState, RemoteUserAccount> UseAzureAdB2CAuthenticationWithRemoteApiAuthorization(this WebAssemblyHostBuilder builder)
        => builder.Services
            .AddApiClient(options => builder.Configuration.Bind("Api", options))
            .AddMsalAuthentication(options =>
            {
                // The binding refers to the settings of wwwroot/appsetting.json
                // Bind 
                builder.Configuration.Bind("AzureAdB2C", options.ProviderOptions.Authentication);

                // Bind scopes
                builder.Configuration.Bind("Api:Scopes", options.ProviderOptions.DefaultAccessTokenScopes);
            });
}