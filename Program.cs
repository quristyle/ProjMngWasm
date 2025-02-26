using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using ProjMngWasm;
using ProjMngWasm.Services;
using Radzen;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");


builder.Services.AddAuthorizationCore();



builder.Services.AddSingleton<ISessionStorageService, SessionStorageService>();
builder.Services.AddScoped<AuthenticationStateProvider, AuthStateProvider>();

//builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });
builder.Services.AddRadzenComponents();
builder.Services.AddRadzenQueryStringThemeService();

builder.Services.AddHttpClient<IUMSService, UMSService>(client => {
                client.BaseAddress = new Uri("https://nums.api.hanjucorp.co.kr");
            });


builder.Services.AddScoped<ExampleService>();
builder.Services.AddSingleton<GitHubService>();

//builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri("https://nums.hanjucorp.co.kr") });

var app = builder.Build();

   //         await builder.Build().RunAsync();

            app.RunAsync();















