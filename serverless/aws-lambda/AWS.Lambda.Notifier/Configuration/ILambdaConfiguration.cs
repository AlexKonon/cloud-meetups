namespace AWS.Lambda.Notifier.Configuration
{
    public interface ILambdaConfiguration
    {
        string TelegramApiBaseAddress { get; set; }

        string ChanelName { get; set; }

        string BotId { get; set; }
    }
}