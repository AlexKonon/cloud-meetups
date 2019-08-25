namespace Azure.Function.Notifier.Configuration
{
    public interface IFunctionConfiguration
    {
        string TelegramApiBaseAddress { get; set; }

        string ChanelName { get; set; }

        string BotId { get; set; }
    }
}