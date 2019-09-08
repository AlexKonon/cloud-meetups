using System.Net;
using Amazon.Lambda.APIGatewayEvents;
using AWS.Lambda.Notifier.Operation;

namespace AWS.Lambda.Notifier.Factories
{
    public interface IApiGatewayProxyResponseFactory
    {
        APIGatewayProxyResponse Create(OperationStatus status);
    }
}