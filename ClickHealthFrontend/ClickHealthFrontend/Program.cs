using ClickHealthFrontend;
using ClickHealthFrontend.Auth;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using System.Net.Http;

var builder = WebAssemblyHostBuilder.CreateDefault(args);

// Root components
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

// HttpClient for API calls to backend
builder.Services.AddScoped(sp =>
{
    var client = new HttpClient
    {
        BaseAddress = new Uri("https://localhost:7286") // Backend URL
    };

    // Optional: default headers
    client.DefaultRequestHeaders.Add("Accept", "application/json");
    return client;
});

// Authentication and Authorization
builder.Services.AddScoped<AuthenticationStateProvider, CustomAuthStateProvider>();
builder.Services.AddAuthorizationCore();

await builder.Build().RunAsync();
