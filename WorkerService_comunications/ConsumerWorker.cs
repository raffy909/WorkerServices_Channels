using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Channels;
using System.Threading.Tasks;

namespace WorkerService_comunications
{
    class ConsumerWorker : BackgroundService
    {
        private readonly ILogger<ConsumerWorker> _logger;
        private readonly Channel<int> _channel;

        public ConsumerWorker(
            ILogger<ConsumerWorker> logger,
            Channel<int> channel)
        {
            _logger = logger;
            _channel = channel;
        }
        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                int msg = await _channel.Reader.ReadAsync();
                _logger.LogInformation("{time}: Consumer recived: {msg}", DateTimeOffset.Now, msg);
            }
        }
    }
}
