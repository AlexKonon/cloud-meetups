using System;
using System.Threading;
using System.Threading.Tasks;
using AWS.Lambda.Notifier.Configuration;
using AWS.Lambda.Notifier.Factories;
using AWS.Lambda.Notifier.Http;

namespace AWS.Lambda.Notifier.Services
{
    public sealed class TelegramService : ITelegramService
    {
        private readonly ILambdaConfiguration _lambdaConfiguration;
        private readonly ITelegramBotMessageFactory _telegramBotMessageFactory;
        private readonly IApiCollaborator _apiCollaborator;

        public TelegramService(
            ILambdaConfiguration lambdaConfiguration,
            ITelegramBotMessageFactory telegramBotMessageFactory,
            IApiCollaborator apiCollaborator)
        {
            _lambdaConfiguration = lambdaConfiguration ?? throw new ArgumentNullException(nameof(lambdaConfiguration));
            _telegramBotMessageFactory = telegramBotMessageFactory ?? throw new ArgumentNullException(nameof(telegramBotMessageFactory));
            _apiCollaborator = apiCollaborator ?? throw new ArgumentNullException(nameof(apiCollaborator));
        }

        public async Task SendChanelMessage(string message)
        {
            if (string.IsNullOrWhiteSpace(message))
                throw new ArgumentException("Value cannot be null or whitespace.", nameof(message));

            var uri = $"/{_lambdaConfiguration.BotId}/sendMessage";

            var telegramBotMessage = _telegramBotMessageFactory.Create(_lambdaConfiguration.ChanelName, message);

            await _apiCollaborator.PostAsync(HttpClientType.Telegram, uri, telegramBotMessage, CancellationToken.None);
        }
    }
}