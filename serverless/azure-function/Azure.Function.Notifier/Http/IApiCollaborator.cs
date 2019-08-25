using System.Threading;
using System.Threading.Tasks;

namespace Azure.Function.Notifier.Http
{
    public interface IApiCollaborator
    {
        Task PostAsync(HttpClientType clientType, string uri, object content, CancellationToken cancellationToken);
    }
}