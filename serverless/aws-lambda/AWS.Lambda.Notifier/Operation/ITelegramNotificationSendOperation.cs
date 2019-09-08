using System.Threading.Tasks;
using Amazon.Lambda.APIGatewayEvents;

namespace AWS.Lambda.Notifier.Operation
{
    public interface ITelegramNotificationSendOperation
    {
        Task<OperationStatus> ExecuteAsync(APIGatewayProxyRequest request);
    }
}