using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.WebAssembly.Authentication;
using System.Net.Http.Headers;


namespace telco.Extensions;


public class ApiAuthorizationMessageHandler : AuthorizationMessageHandler
{
    /**********/
    /* Fields */
    /**********/

    private readonly IAccessTokenProvider _provider;

    /***************/
    /* Constructor */
    /***************/

    public ApiAuthorizationMessageHandler(IAccessTokenProvider provider, NavigationManager navigation, ApiClientOptions parameters) : base(provider, navigation)
    {
        _provider = provider;
        ConfigureHandler(
            authorizedUrls: new[] { parameters.BaseAddress },
            scopes: parameters.Scopes);
    }

    /***********/
    /* Methods */
    /***********/

    protected override async  Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
    {
        // Get the access token from the provider.
        var accessTokenResult = await _provider.RequestAccessToken();

        if (accessTokenResult.TryGetToken(out var accessToken))
        {
            // Add the authorization header to the request.
            request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", accessToken.Value);
        }

        return await base.SendAsync(request, cancellationToken);
    }
}