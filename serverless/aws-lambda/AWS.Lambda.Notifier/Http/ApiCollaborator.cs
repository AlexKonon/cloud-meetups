using System;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Amazon.Lambda.Core;

namespace AWS.Lambda.Notifier.Http
{
    public sealed class ApiCollaborator : IApiCollaborator
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly IHttpRequestFactory _httpRequestFactory;

        public ApiCollaborator(
            IHttpClientFactory httpClientFactory,
            IHttpRequestFactory httpRequestFactory)
        {
            _httpClientFactory = httpClientFactory ?? throw new ArgumentNullException(nameof(httpClientFactory));
            _httpRequestFactory = httpRequestFactory ?? throw new ArgumentNullException(nameof(httpRequestFactory));
        }

        public async Task PostAsync(HttpClientType clientType, string uri, object content, CancellationToken cancellationToken)
        {
            if (string.IsNullOrWhiteSpace(uri))
                throw new ArgumentException("Value cannot be null or whitespace.", nameof(uri));
            if (content == null)
                throw new ArgumentNullException(nameof(content));

            try
            {
                var client = _httpClientFactory.CreateClient(clientType.ToString());

                using (var request = _httpRequestFactory.CreateForPost(uri, content))
                {
                    using (var response = await client
                        .SendAsync(request, HttpCompletionOption.ResponseHeadersRead, cancellationToken)
                        .ConfigureAwait(false))
                    {
                        response.EnsureSuccessStatusCode();
                    }
                }
            }
            catch (ArgumentNullException)
            {
                LambdaLogger.Log($"Provided http request could not be empty, HttpClient={clientType}, Uri={uri}");
            }
            catch (InvalidOperationException)
            {
                LambdaLogger.Log($"The request message was already sent, HttpClient={clientType}, Uri={uri}");
            }
            catch (HttpRequestException)
            {
                LambdaLogger.Log($"The request sending has been failed, HttpClient={clientType}, Uri={uri}");
            }
            catch (Exception)
            {
                LambdaLogger.Log($"An error occured while trying to send [POST] request, HttpClient={clientType}, Uri={uri}");
            }
        }
    }
}