using System;
using System.Net.Http.Headers;
using Azure.Function.Notifier;
using Azure.Function.Notifier.Configuration;
using Azure.Function.Notifier.Factories;
using Azure.Function.Notifier.Http;
using Azure.Function.Notifier.Services;
using Microsoft.Azure.Functions.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

[assembly: FunctionsStartup(typeof(Startup))]

namespace Azure.Function.Notifier
{
    public sealed class Startup : FunctionsStartup
    {
        public override void Configure(IFunctionsHostBuilder builder)
        {
            var configuration = BuildFunctionSettings();
            builder.Services.AddSingleton(configuration);

            builder.Services.AddLogging();

            builder.Services.AddTransient<ITelegramService, TelegramService>();
            builder.Services.AddSingleton<ITelegramBotMessageFactory, TelegramBotMessageFactory>();

            builder.Services.AddTransient<IApiCollaborator, ApiCollaborator>();
            builder.Services.AddSingleton<IHttpRequestFactory, HttpRequestFactory>();
            builder.Services.AddSingleton<IStreamHttpContentFactory, StreamHttpContentFactory>();

            builder.Services.AddHttpClient(HttpClientType.Telegram.ToString(), client =>
            {
                client.BaseAddress = new Uri(configuration.TelegramApiBaseAddress);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            });
        }

        private static IFunctionConfiguration BuildFunctionSettings()
        {
            var configuration = new FunctionConfiguration();

            var config = new ConfigurationBuilder()
                .AddEnvironmentVariables()
                .Build();

            config.Bind(configuration);

            return configuration;
        }
    }
}