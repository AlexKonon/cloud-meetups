using System;
using System.Net;
using Amazon.Lambda.APIGatewayEvents;
using AWS.Lambda.Notifier.Operation;

namespace AWS.Lambda.Notifier.Factories
{
    public sealed class ApiGatewayProxyResponseFactory : IApiGatewayProxyResponseFactory
    {
        public APIGatewayProxyResponse Create(HttpStatusCode statusCode)
        {
            return new APIGatewayProxyResponse
            {
                StatusCode = (int) statusCode
            };
        }

        public APIGatewayProxyResponse Create(OperationStatus status)
        {
            return new APIGatewayProxyResponse
            {
                StatusCode = (int) GetStatusCodeByOperationStatus(status)
            };
        }

        private static HttpStatusCode GetStatusCodeByOperationStatus(OperationStatus status)
        {
            switch (status)
            {
                case OperationStatus.BadRequest:
                    return HttpStatusCode.BadRequest;
                case OperationStatus.Success:
                    return HttpStatusCode.OK;
                case OperationStatus.Error:
                    return HttpStatusCode.InternalServerError;
                default:
                    throw new NotSupportedException("Operation status is not supported yet.");
            }
        }
    }
}
