using System.Threading;
using System.Threading.Tasks;

namespace Azure.Function.Notifier.Services
{
    public interface ITelegramService
    {
        Task SendChanelMessageAsync(string message, CancellationToken cancellationToken);
    }
}
