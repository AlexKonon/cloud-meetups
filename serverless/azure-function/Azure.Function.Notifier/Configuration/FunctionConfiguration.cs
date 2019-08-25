namespace Azure.Function.Notifier.Configuration
{
    public sealed class FunctionConfiguration : IFunctionConfiguration
    {
        public string TelegramApiBaseAddress { get; set; }

        public string ChanelName { get; set; }

        public string BotId { get; set; }
    }
}
