using ChatGPTApp.UI.Core.Interfaces;
using ChatGPTApp.UI.Core.Services;

namespace ChatGPTApp.UI.Mobile;

public static class MauiProgram
{
    public static MauiApp CreateMauiApp()
    {
        var builder = MauiApp.CreateBuilder();
        builder
            .UseMauiApp<App>()
            .ConfigureFonts(fonts =>
            {
                fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
            });

        builder.Services.AddMauiBlazorWebView();
#if DEBUG
        builder.Services.AddBlazorWebViewDeveloperTools();
#endif
        HttpClientHandler insecureHandler = GetInsecureHandler();
        HttpClient httpClient = new HttpClient(insecureHandler);
        builder.Services.AddScoped<IOpenAIService>(_ => new OpenAIService());

        return builder.Build();
    }
    public static HttpClientHandler GetInsecureHandler()
    {
        HttpClientHandler handler = new HttpClientHandler();
        handler.ServerCertificateCustomValidationCallback = (message, cert, chain, errors) =>
        {
            if (cert.Issuer.Equals("CN=192.168.101.254"))
                return true;
            return errors == System.Net.Security.SslPolicyErrors.None;
        };
        return handler;
    }

}
