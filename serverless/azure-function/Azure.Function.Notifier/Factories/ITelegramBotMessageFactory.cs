using Azure.Function.Notifier.Domain;

namespace Azure.Function.Notifier.Factories
{
    public interface ITelegramBotMessageFactory
    {
        TelegramBotMessage Create(string chanelName, string message);
    }
}