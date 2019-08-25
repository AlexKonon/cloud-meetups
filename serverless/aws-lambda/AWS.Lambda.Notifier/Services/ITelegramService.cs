﻿using System.Threading;
using System.Threading.Tasks;

namespace AWS.Lambda.Notifier.Services
{
    interface ITelegramService
    {
        Task SendChanelMessageAsync(string message, CancellationToken cancellationToken);
    }
}
