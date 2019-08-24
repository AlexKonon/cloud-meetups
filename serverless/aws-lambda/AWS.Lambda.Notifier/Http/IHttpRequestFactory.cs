using System.Net.Http;

namespace AWS.Lambda.Notifier.Http
{
    public interface IHttpRequestFactory
    {
        HttpRequestMessage CreateForPost(string uri, object content);
    }
}