using AWS.Lambda.Notifier.Domain;

namespace AWS.Lambda.Notifier.Factories
{
    public interface ITelegramBotMessageFactory
    {
        TelegramBotMessage Create(string chanelName, string message);
    }
}