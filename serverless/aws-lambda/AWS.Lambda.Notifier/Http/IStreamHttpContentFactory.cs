using System.Net.Http;

namespace AWS.Lambda.Notifier.Http
{
    public interface IStreamHttpContentFactory
    {
        HttpContent Create(object content);
    }
}