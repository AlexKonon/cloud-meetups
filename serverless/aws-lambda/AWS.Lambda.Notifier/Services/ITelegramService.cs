using System.Threading.Tasks;

namespace AWS.Lambda.Notifier.Services
{
    interface ITelegramService
    {
        Task SendChanelMessage(string message);
    }
}
