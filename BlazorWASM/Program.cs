using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using BlazorWASM;
using BlazorWASM.Auth;
using Domain.Auth;
using HttpClient.ClientInterfaces;
using HttpClient.Implementations;
using Microsoft.AspNetCore.Components.Authorization;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

AuthorizationPolicies.AddPolicies(builder.Services);

builder.Services.AddScoped<IUserService, UserHttpClient>();
builder.Services.AddScoped<IPostService, PostHttpClient>();
builder.Services.AddScoped<AuthenticationStateProvider, CustomAuthProvider>();

builder.Services.AddScoped(sp => new System.Net.Http.HttpClient { BaseAddress = new Uri("https://localhost:7283") });

await builder.Build().RunAsync();