using FitLife.Frontend;
using FitLife.Frontend.Services;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

var identityUrl = builder.Configuration["IdentityBaseUrl"] ?? "http://localhost:5244/";
var classesUrl = builder.Configuration["ClassesBaseUrl"] ?? "http://localhost:5245/";
var trainerUrl = builder.Configuration["TrainerBaseUrl"] ?? "http://localhost:5120/";

builder.Services.AddHttpClient<AuthService>(client =>
    client.BaseAddress = new Uri(identityUrl));

builder.Services.AddHttpClient<ClassesService>(client =>
    client.BaseAddress = new Uri(classesUrl));

builder.Services.AddHttpClient<TrainerService>(client =>
    client.BaseAddress = new Uri(trainerUrl));

await builder.Build().RunAsync();