using System;
using Azure.Function.Notifier.Domain;

namespace Azure.Function.Notifier.Factories
{
    public sealed class TelegramBotMessageFactory : ITelegramBotMessageFactory
    {
        public TelegramBotMessage Create(string chanelName, string message)
        {
            if (string.IsNullOrWhiteSpace(chanelName))
                throw new ArgumentException("Value cannot be null or whitespace.", nameof(chanelName));
            if (string.IsNullOrWhiteSpace(message))
                throw new ArgumentException("Value cannot be null or whitespace.", nameof(message));

            return new TelegramBotMessage
            {
                chat_id = chanelName,
                text = message
            };
        }
    }
}
