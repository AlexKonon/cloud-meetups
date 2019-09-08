using System;
using System.ComponentModel.DataAnnotations;
using Amazon.Lambda.APIGatewayEvents;

namespace AWS.Lambda.Notifier.Validators
{
    public sealed class ApiGatewayProxyRequestValidator : IApiGatewayProxyRequestValidator
    {
        public void Validate(APIGatewayProxyRequest apiGatewayProxyRequest)
        {
            if (apiGatewayProxyRequest == null)
                throw new ArgumentNullException(nameof(apiGatewayProxyRequest));

            if (string.IsNullOrWhiteSpace(apiGatewayProxyRequest.Body))
                throw new ValidationException();
        }
    }
}
