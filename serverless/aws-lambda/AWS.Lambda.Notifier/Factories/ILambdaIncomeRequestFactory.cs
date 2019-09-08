using Amazon.Lambda.APIGatewayEvents;
using AWS.Lambda.Notifier.Domain;

namespace AWS.Lambda.Notifier.Factories
{
    public interface ILambdaIncomeRequestFactory
    {
        LambdaIncomeRequest CreateFrom(string requestBody);
    }
}