using System;
using System.Threading;
using System.Threading.Tasks;
using Amazon.Lambda.Core;
using AWS.Lambda.Notifier.Domain;
using AWS.Lambda.Notifier.Services;
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

        public async Task LambdaEntry(LambdaIncomeRequest request, ILambdaContext context)
        {
            if (request == null)
                throw new ArgumentNullException(nameof(request));
            if (context == null)
                throw new ArgumentNullException(nameof(context));

            try
            {
                var telegramService =_serviceProvider.GetService<ITelegramService>();

                await telegramService.SendChanelMessageAsync(request.Content, CancellationToken.None);
            }
            catch (Exception)
            {
                LambdaLogger.Log($"An error occured while trying to send Telegram notification, RequestId={context.AwsRequestId}");
            }
        }
    }
}
