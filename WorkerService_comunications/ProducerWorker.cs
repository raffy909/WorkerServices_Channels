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
    class ProducerWorker : BackgroundService
    {
        private readonly ILogger<ProducerWorker> _logger;
        private readonly Channel<int> _channel;
        private readonly Random _random; 

        public ProducerWorker(
            ILogger<ProducerWorker> logger,
            Channel<int> channel
            )
        {
            _logger = logger;
            _channel = channel;

            _random = new Random();
        }
        
        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                int msg = _random.Next();
                _channel.Writer.TryWrite(msg);
                _logger.LogInformation("{time}: Producer sended: {msg}", DateTimeOffset.Now, msg);
                
                await Task.Delay(5000, stoppingToken);
            }
        }
    }
}
