using Amazon.Lambda.APIGatewayEvents;

namespace AWS.Lambda.Notifier.Validators
{
    public interface IApiGatewayProxyRequestValidator
    {
        void Validate(APIGatewayProxyRequest apiGatewayProxyRequest);
    }
}