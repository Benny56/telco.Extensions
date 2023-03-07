using Microsoft.AspNetCore.Components.WebAssembly.Authentication;
using Microsoft.Extensions.DependencyInjection;


namespace telco.Extensions;


public static class MsalWebAssemblyServiceCollectionExtensions
{
    /// <summary>
    /// Adds a http client with the base address of an API endpoint which handles the access token obtained from a <see cref="IAccessTokenProvider"/> 
    /// </summary>
    public static IServiceCollection AddApiClient(this IServiceCollection services, Action<ApiClientOptions> configure)
    {
        var options = new ApiClientOptions();
        configure(options);

        if (options.BaseAddress == null)
            throw new ArgumentNullException("Missing API base address", nameof(options.BaseAddress));

        if (options.HttpClientName == null)
            throw new ArgumentNullException("Missing an identifying name for the http client", nameof(options.HttpClientName));

        services
            .AddTransient(_ => options)
            .AddTransient<ApiAuthorizationMessageHandler>()
            .AddHttpClient(options.HttpClientName, client => client.BaseAddress = new Uri(options.BaseAddress))
            .AddHttpMessageHandler<ApiAuthorizationMessageHandler>();

        services.AddScoped(sp => sp.GetRequiredService<IHttpClientFactory>().CreateClient(options.HttpClientName));
        return services;
    }
}
