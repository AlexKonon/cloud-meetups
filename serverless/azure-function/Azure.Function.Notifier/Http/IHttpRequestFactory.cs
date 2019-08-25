using System.Net.Http;

namespace Azure.Function.Notifier.Http
{
    public interface IHttpRequestFactory
    {
        HttpRequestMessage CreateForPost(string uri, object content);
    }
}