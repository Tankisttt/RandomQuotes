using System;
using System.Net.Http;
using System.Net.Sockets;
using Microsoft.Extensions.DependencyInjection;
using Polly;
using Refit;

namespace RandomQuotes.Sdk.Extensions
{
    public static class RandomQuotesExtensions
    {
        private const string HttpClientName = "RandomQuotesService";

        public static void RegisterQuotesApiClients(this IServiceCollection services, string url) =>
            RegisterQuotesApiClients(services, new Uri(url));

        public static void RegisterQuotesApiClients(this IServiceCollection services, Uri uri)
        {
            //add http client registration with retry
            services.AddHttpClient(HttpClientName, config =>
            {
                config.BaseAddress = uri;
            }).AddTransientHttpErrorPolicy(p =>
                p.Or<SocketException>().WaitAndRetryAsync(3, i => TimeSpan.FromMilliseconds(500 * (i + 1))));

            services.AddRestClient<IQuotes>();
        }

        private static IServiceCollection AddRestClient<T>(this IServiceCollection services) where T : class
        {
            services.AddSingleton(_ => RequestBuilder.ForType<T>());
            services.AddSingleton(provider => RestService.For(
                provider.GetRequiredService<IHttpClientFactory>().CreateClient(HttpClientName),
                provider.GetRequiredService<IRequestBuilder<T>>()));
            return services;
        }
    }
}