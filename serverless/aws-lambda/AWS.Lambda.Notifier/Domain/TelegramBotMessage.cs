namespace AWS.Lambda.Notifier.Domain
{
    public sealed class TelegramBotMessage
    {
        public string chat_id { get; set; }

        public string text { get; set; }
    }
}
