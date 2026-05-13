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
var trainingLogUrl = builder.Configuration["TrainingLogBaseUrl"] ?? "http://localhost:5084/";
var livestreamUrl = builder.Configuration["LivestreamBaseUrl"] ?? "http://localhost:5280/";
var communityUrl = builder.Configuration["CommunityBaseUrl"] ?? "http://localhost:5246/";
var membershipUrl = builder.Configuration["MembershipBaseUrl"] ?? "http://localhost:5154/";

builder.Services.AddHttpClient<AuthService>(client =>
    client.BaseAddress = new Uri(identityUrl));

builder.Services.AddHttpClient<ClassesService>(client =>
    client.BaseAddress = new Uri(classesUrl));

builder.Services.AddHttpClient<TrainerService>(client =>
    client.BaseAddress = new Uri(trainerUrl));

builder.Services.AddHttpClient<TrainingLogService>(client =>
    client.BaseAddress = new Uri(trainingLogUrl));

builder.Services.AddHttpClient<LivestreamService>(client =>
    client.BaseAddress = new Uri(livestreamUrl));

builder.Services.AddHttpClient<CommunityService>(client =>
    client.BaseAddress = new Uri(communityUrl));

builder.Services.AddHttpClient<MembershipService>(client =>
    client.BaseAddress = new Uri(membershipUrl));

await builder.Build().RunAsync();