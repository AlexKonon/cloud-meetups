using System.Net.Http;

namespace Azure.Function.Notifier.Http
{
    public interface IStreamHttpContentFactory
    {
        HttpContent Create(object content);
    }
}