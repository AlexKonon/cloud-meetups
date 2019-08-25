using System;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;

namespace Azure.Function.Notifier.Http
{
    public sealed class ApiCollaborator : IApiCollaborator
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly IHttpRequestFactory _httpRequestFactory;
        private readonly ILogger<ApiCollaborator> _logger;

        public ApiCollaborator(
            IHttpClientFactory httpClientFactory,
            IHttpRequestFactory httpRequestFactory,
            ILogger<ApiCollaborator> logger)
        {
            _httpClientFactory = httpClientFactory ?? throw new ArgumentNullException(nameof(httpClientFactory));
            _httpRequestFactory = httpRequestFactory ?? throw new ArgumentNullException(nameof(httpRequestFactory));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public Task PostAsync(
            HttpClientType clientType,
            string uri,
            object content,
            CancellationToken cancellationToken)
        {
            if (string.IsNullOrWhiteSpace(uri))
                throw new ArgumentException("Value cannot be null or whitespace.", nameof(uri));
            if (content == null)
                throw new ArgumentNullException(nameof(content));

            return DoPostAsync(clientType, uri, content, cancellationToken);
        }

        private async Task DoPostAsync(
            HttpClientType clientType,
            string uri,
            object content,
            CancellationToken cancellationToken)
        {
            try
            {
                var client = _httpClientFactory.CreateClient(clientType.ToString());

                using (var request = _httpRequestFactory.CreateForPost(uri, content))
                {
                    using (var response = await client.SendAsync(request, HttpCompletionOption.ResponseHeadersRead, cancellationToken))
                    {
                        response.EnsureSuccessStatusCode();
                    }
                }
            }
            catch (ArgumentNullException ex)
            {
                _logger.LogError($"Provided http request could not be empty, HttpClient={clientType}, Uri={uri}", ex);
            }
            catch (InvalidOperationException ex)
            {
                _logger.LogError($"The request message was already sent, HttpClient={clientType}, Uri={uri}", ex);
            }
            catch (HttpRequestException ex)
            {
                _logger.LogError($"The request sending has been failed, HttpClient={clientType}, Uri={uri}", ex);
            }
            catch (OperationCanceledException ex)
            {
                _logger.LogError($"An operation was canceled, HttpClient={clientType}, Uri={uri}", ex);
            }
            catch (Exception ex)
            {
                _logger.LogError(
                    $"An error occured while trying to send [POST] request, HttpClient={clientType}, Uri={uri}", ex);
            }
        }
    }
}