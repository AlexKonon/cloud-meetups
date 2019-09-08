using System;
using System.Threading.Tasks;
using Amazon.Lambda.APIGatewayEvents;
using Amazon.Lambda.Core;
using AWS.Lambda.Notifier.Factories;
using AWS.Lambda.Notifier.Operation;
using Microsoft.Extensions.DependencyInjection;

[assembly: LambdaSerializer(typeof(Amazon.Lambda.Serialization.Json.JsonSerializer))]

namespace AWS.Lambda.Notifier
{
    public sealed class NotifierHandler
    {
        private readonly IServiceProvider _serviceProvider;

        public NotifierHandler(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider ?? throw new ArgumentNullException(nameof(serviceProvider));
        }
        public NotifierHandler() : this(StartUp.Container.BuildServiceProvider())
        {
        }

        public async Task<APIGatewayProxyResponse> LambdaEntry(
            APIGatewayProxyRequest apiGatewayProxyRequest,
            ILambdaContext context)
        {
            if (apiGatewayProxyRequest == null)
                throw new ArgumentNullException(nameof(apiGatewayProxyRequest));
            if (context == null)
                throw new ArgumentNullException(nameof(context));

            var operation = _serviceProvider.GetService<ITelegramNotificationSendOperation>();
            var responseFactory = _serviceProvider.GetService<IApiGatewayProxyResponseFactory>();

            var operationResult = await operation.ExecuteAsync(apiGatewayProxyRequest);

            return responseFactory.Create(operationResult);
        }
    }
}
