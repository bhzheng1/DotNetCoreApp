using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using System.Net.Http.Headers;
using System.Net.Mime;
using Polly;
using Polly.Extensions.Http;

namespace WebApi_Client;

public static partial class ServiceCollectionExtentions
{
    public static IServiceCollection AddTokenClient(this IServiceCollection services)
    {
        services.AddMemoryCache();
        services.AddOptions<TokenOptions>().Configure<IConfiguration>((o, c) =>
        {
            c.GetSection(TokenOptions.SectionName).Bind(o);
        });
        services.AddHttpClient<ITokenApiClient, TokenApiClient>().ConfigureHttpClient((s, c) =>
        {
            var tokenOptions = s.GetRequiredService<IOptions<TokenOptions>>().Value;
            c.BaseAddress = new Uri(tokenOptions.BaseUri);
            c.DefaultRequestHeaders.Accept.Clear();
            c.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/x-www-form-urlencoded"));
        });
        return services;
    }

    public static IServiceCollection AddWebApiClient(this IServiceCollection services)
    {
        services.AddOptions<WebApiOptions>().Configure<IConfiguration>((o, c) =>
        {
            c.GetSection(WebApiOptions.SectionName).Bind(o);
        });

        services.AddHttpClient<IWebApiClient, WebApiApiClient>().ConfigureHttpClient((s, c) =>
        {
            var apiOptions = s.GetRequiredService<IOptions<WebApiOptions>>().Value;
            c.BaseAddress = new Uri(apiOptions.BaseUri);
            c.DefaultRequestHeaders.Accept.Clear();
            c.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue(MediaTypeNames.Application.Json));
        })
            .AddHttpMessageHandler<AuthHeaderHandler>()
            .AddPolicyHandler(GetRetryPolicy())
            .SetHandlerLifetime(TimeSpan.FromMinutes(5));
        return services;
    }
    static IAsyncPolicy<HttpResponseMessage> GetRetryPolicy()
    {
        return HttpPolicyExtensions
            .HandleTransientHttpError()
            .OrResult(msg => msg.StatusCode == System.Net.HttpStatusCode.NotFound)
            .WaitAndRetryAsync(6, retryAttempt => TimeSpan.FromSeconds(Math.Pow(2, retryAttempt)));
    }
}

