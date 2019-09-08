using System;
using System.ComponentModel.DataAnnotations;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using Amazon.Lambda.APIGatewayEvents;
using Amazon.Lambda.Core;
using AWS.Lambda.Notifier.Factories;
using AWS.Lambda.Notifier.Services;
using AWS.Lambda.Notifier.Validators;

namespace AWS.Lambda.Notifier.Operation
{
    public sealed class TelegramNotificationSendOperation : ITelegramNotificationSendOperation
    {
        private readonly IApiGatewayProxyRequestValidator _apiGatewayProxyRequestValidator;
        private readonly ILambdaIncomeRequestFactory _lambdaIncomeRequestFactory;
        private readonly ITelegramService _telegramService;

        public TelegramNotificationSendOperation(
            IApiGatewayProxyRequestValidator apiGatewayProxyRequestValidator,
            ILambdaIncomeRequestFactory lambdaIncomeRequestFactory,
            ITelegramService telegramService)
        {
            _apiGatewayProxyRequestValidator = apiGatewayProxyRequestValidator ?? throw new ArgumentNullException(nameof(apiGatewayProxyRequestValidator));
            _lambdaIncomeRequestFactory = lambdaIncomeRequestFactory ?? throw new ArgumentNullException(nameof(lambdaIncomeRequestFactory));
            _telegramService = telegramService ?? throw new ArgumentNullException(nameof(telegramService));
        }

        public Task<OperationStatus> ExecuteAsync(APIGatewayProxyRequest apiGatewayProxyRequest)
        {
            if (apiGatewayProxyRequest == null)
                throw new ArgumentNullException(nameof(apiGatewayProxyRequest));

            return DoExecuteAsync(apiGatewayProxyRequest);
        }

        private async Task<OperationStatus> DoExecuteAsync(APIGatewayProxyRequest apiGatewayProxyRequest)
        {
            try
            {
                _apiGatewayProxyRequestValidator.Validate(apiGatewayProxyRequest);

                var request = _lambdaIncomeRequestFactory.CreateFrom(apiGatewayProxyRequest.Body);

                if (string.IsNullOrWhiteSpace(request?.Content))
                {
                    LambdaLogger.Log("Received proxy request body content is invalid.");

                    return OperationStatus.BadRequest;
                }

                await _telegramService.SendChanelMessageAsync(request.Content, CancellationToken.None);

                return OperationStatus.Success;
            }
            catch (ValidationException)
            {
                LambdaLogger.Log("Invalid request is received.");

                return OperationStatus.BadRequest;
            }
            catch (Exception)
            {
                LambdaLogger.Log("An error occured while trying to send Telegram notification.");

                return OperationStatus.Error;
            }
        }
    }
}