using System;
using System.Threading;
using System.Threading.Tasks;
using Azure.Function.Notifier.Configuration;
using Azure.Function.Notifier.Factories;
using Azure.Function.Notifier.Http;

namespace Azure.Function.Notifier.Services
{
    public sealed class TelegramService : ITelegramService
    {
        private readonly IFunctionConfiguration _functionConfiguration;
        private readonly ITelegramBotMessageFactory _telegramBotMessageFactory;
        private readonly IApiCollaborator _apiCollaborator;

        public TelegramService(
            IFunctionConfiguration functionConfiguration,
            ITelegramBotMessageFactory telegramBotMessageFactory,
            IApiCollaborator apiCollaborator)
        {
            _functionConfiguration = functionConfiguration ?? throw new ArgumentNullException(nameof(functionConfiguration));
            _telegramBotMessageFactory = telegramBotMessageFactory ?? throw new ArgumentNullException(nameof(telegramBotMessageFactory));
            _apiCollaborator = apiCollaborator ?? throw new ArgumentNullException(nameof(apiCollaborator));
        }

        public Task SendChanelMessageAsync(string message, CancellationToken cancellationToken)
        {
            if (string.IsNullOrWhiteSpace(message))
                throw new ArgumentException("Value cannot be null or whitespace.", nameof(message));

            return DoSendChanelMessageAsync(message, cancellationToken);
        }

        private async Task DoSendChanelMessageAsync(string message, CancellationToken cancellationToken)
        {
            var uri = $"/{_functionConfiguration.BotId}/sendMessage";

            var telegramBotMessage = _telegramBotMessageFactory.Create(_functionConfiguration.ChanelName, message);

            await _apiCollaborator.PostAsync(HttpClientType.Telegram, uri, telegramBotMessage, cancellationToken);
        }
    }
}