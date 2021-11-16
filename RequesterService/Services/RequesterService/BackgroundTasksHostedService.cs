using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace RequesterService.Services.RequesterService
{
    public interface IScopedProcessingService
    {
        Task Run(Action action, int millisecondsdelay);
        void Stop();
        string GetServiceStatus();
    }

    public class BackgroundTasksHostedService : IScopedProcessingService, IHostedService
    {
        int executionCount = 0;
        readonly ILogger<BackgroundTasksHostedService> logger;
        CancellationTokenSource cancelSource;
        CancellationToken stoppingToken;
        private Task Runner;
        private static System.Timers.Timer aTimer;

        public string GetServiceStatus()
        {
            return Runner == null ? "Deactivated and suspended" : "Аctivated and launched";
        }

        async Task IScopedProcessingService.Run(Action action, int millisecondsdelay)
        {
            if (Runner == null)
                Runner = Task.Run(() => taskToRunAsync(action, millisecondsdelay));
        }

        private async Task taskToRunAsync(Action action, int millisecondsdelay)
        {
            cancelSource = new CancellationTokenSource();
            stoppingToken = cancelSource.Token;
            while (!stoppingToken.IsCancellationRequested)
            {
                Task.Run(action);
                executionCount++;
                logger.LogInformation($"Worker launched. {executionCount}");
                await Task.Delay(millisecondsdelay, stoppingToken);
            }            
        }

        void IScopedProcessingService.Stop()
        {
            if (Runner != null)
            {
                cancelSource.Cancel();
                logger.LogInformation("The worker terminated");
                Runner = null;
            }
                
        }

        public BackgroundTasksHostedService(ILogger<BackgroundTasksHostedService> logger)
        {
            this.logger = logger;
        }

        public void Dispose()
        {
            throw new NotImplementedException();
        }

        public Task StartAsync(CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }
    }
}
