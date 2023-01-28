using ChatGPTApp.UI.Core.Interfaces;
using ChatGPTApp.UI.Core.Services;
using ChatGPTApp.UI.Web;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");
builder.Services.AddHttpClient();

builder.Services.AddScoped<IOpenAIService>(_ => new OpenAIService());

await builder.Build().RunAsync();
