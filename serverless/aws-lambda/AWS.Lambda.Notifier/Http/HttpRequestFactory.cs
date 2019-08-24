using System;
using System.Net.Http;

namespace AWS.Lambda.Notifier.Http
{
    public sealed class HttpRequestFactory : IHttpRequestFactory
    {
        private readonly IStreamHttpContentFactory _streamHttpContentFactory;

        public HttpRequestFactory(IStreamHttpContentFactory streamHttpContentFactory)
        {
            _streamHttpContentFactory = streamHttpContentFactory ?? throw new ArgumentNullException(nameof(streamHttpContentFactory));
        }

        public HttpRequestMessage CreateForPost(string uri, object content)
        {
            if (string.IsNullOrWhiteSpace(uri))
                throw new ArgumentException("Value cannot be null or whitespace.", nameof(uri));
            if (content == null)
                throw new ArgumentNullException(nameof(content));

            return new HttpRequestMessage(HttpMethod.Post, uri)
            {
                Content = _streamHttpContentFactory.Create(content)
            };
        }
    }
}
