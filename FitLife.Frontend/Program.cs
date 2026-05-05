using FitLife.Frontend;
using FitLife.Frontend.Services;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

var identityUrl = builder.Configuration["IdentityBaseUrl"] ?? "http://localhost:5244/";
builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(identityUrl) });
builder.Services.AddScoped<AuthService>();

await builder.Build().RunAsync();