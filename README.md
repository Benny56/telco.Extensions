# Blazor WebAssembly Authentication/Authorization with Azure Active Directory B2C

## Description

This .NET 7 project adds authentication to a Blazor WebAssembly application using Azure Active Directory B2C.
The application retrieves an access token from the Azure Active Directory B2C instance and uses it to generate a Bearer Token Authorization Header for Http Client requests.
An Http client for API requests is provided.   

## Getting started

1.  Create a new Azure Active Directory B2C tenant or use an existing one.
2.  Register your API app and expose at least one scope.
3.  Register your Blazor WebAssembly App and grant access to the API app.
4.  Create or modify an appsettings.json file (see below) and replace the placeholders with the values from Azure Active Directory B2C.
5.  An API based on ASP.NET Core already provides authorization mechanisms. If your API is based on Azure Functions you should provide a Middleware to validate the access token.

### Settings

appsettings.js

    {
      "AzureAdB2C": {
        "Authority": "https://<TenantName>.b2clogin.com/<TenantName>.onmicrosoft.com/<SIGN UP SIGN IN POLICY>",
        "ClientId": "CLIENT ID",                    /* Id of Blazor WebAssembly App */
        "ValidateAuthority": false
      },
      "Api": {
        "HttpClientName": "<DEFAULT = ApiClient>",  /* Omit this entry if you want to use the default name */  
        "BaseAddress": "<API URL>",                 /* Example: https://api.contoso.com */
        "Scopes": [
          "<SCOPE>"                                 /* Example: https://<TenantName>.onmicrosoft.com/<API-CLIENT-ID>/api_access */
        ]
      }
    }

### Setup

Program.cs

    using Microsoft.AspNetCore.Components.Web;
    using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
    using OfficeAssistant.Web;
    using telco.Extensions.DependencyInjection;

    var builder = WebAssemblyHostBuilder.CreateDefault(args);
    builder.RootComponents.Add<App>("#app");
    builder.RootComponents.Add<HeadOutlet>("head::after");

    // Add authentication and API authorization using Azure Active directory B2C
    builder.UseAzureAdB2CAuthenticationWithRemoteApiAuthorization();

    await builder.Build().RunAsync();

