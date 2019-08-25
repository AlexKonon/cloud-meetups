using System;
using System.IO;
using System.Net.Http.Headers;
using AWS.Lambda.Notifier.Configuration;
using AWS.Lambda.Notifier.Factories;
using AWS.Lambda.Notifier.Http;
using AWS.Lambda.Notifier.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace AWS.Lambda.Notifier
{
    public sealed class StartUp
    {
        public static IServiceCollection Container => ConfigureServices();

        private static IServiceCollection ConfigureServices()
        {
            var services = new ServiceCollection();

            var lambdaConfiguration = BuildLambdaConfiguration();

            services.AddSingleton(lambdaConfiguration);

            services.AddTransient<ITelegramService, TelegramService>();
            services.AddSingleton<ITelegramBotMessageFactory, TelegramBotMessageFactory>();

            services.AddTransient<IApiCollaborator, ApiCollaborator>();
            services.AddSingleton<IHttpRequestFactory, HttpRequestFactory>();
            services.AddSingleton<IStreamHttpContentFactory, StreamHttpContentFactory>();

            services.AddHttpClient(HttpClientType.Telegram.ToString(), client =>
            {
                client.BaseAddress = new Uri(lambdaConfiguration.TelegramApiBaseAddress);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            });

            return services;
        }

        private static ILambdaConfiguration BuildLambdaConfiguration()
        {
            var lambdaConfiguration = new LambdaConfiguration();

            var configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .AddEnvironmentVariables()
                .Build();

            configuration.Bind(lambdaConfiguration);

            return lambdaConfiguration;
        }
    }
}
