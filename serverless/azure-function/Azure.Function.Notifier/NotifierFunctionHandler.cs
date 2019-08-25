using System;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Http;
using Azure.Function.Notifier.Domain;
using Azure.Function.Notifier.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Extensions.Logging;

namespace Azure.Function.Notifier
{
    public sealed class NotifierFunctionHandler
    {
        private readonly ITelegramService _telegramService;
        private readonly ILogger<NotifierFunctionHandler> _logger;

        public NotifierFunctionHandler(ITelegramService telegramService, ILogger<NotifierFunctionHandler> logger)
        {
            _telegramService = telegramService ?? throw new ArgumentNullException(nameof(telegramService));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        [FunctionName("Notifier")]
        public async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "post", Route = null)] FunctionIncomeRequest request,
            CancellationToken cancellationToken)
        {
            if (request == null)
                throw new ArgumentNullException(nameof(request));

            _logger.LogInformation("C# HTTP trigger function processed a request.");

            try
            {
                await _telegramService.SendChanelMessageAsync(request.Content, cancellationToken);
            }
            catch (Exception ex)
            {
                _logger.LogError("An error occured while trying to send Telegram notification", ex);

                return new InternalServerErrorResult();
            }

            return new OkResult();
        }
    }
}
