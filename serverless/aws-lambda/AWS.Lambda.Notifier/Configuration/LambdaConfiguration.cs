namespace AWS.Lambda.Notifier.Configuration
{
    public sealed class LambdaConfiguration : ILambdaConfiguration
    {
        public string TelegramApiBaseAddress { get; set; }

        public string ChanelName { get; set; }

        public string BotId { get; set; }
    }
}
