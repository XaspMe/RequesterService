using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using RequesterService.Exeptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace RequesterService.Services.RequesterService
{
    public interface IScopedProcessingService
    {
        void Run(Action action, int millisecondsdelay);
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

        public string GetServiceStatus()
        {
            return Runner == null ? "Deactivated and suspended" : "Аctivated and launched";
        }

        void IScopedProcessingService.Run(Action action, int millisecondsdelay)
        {
            if (Runner != null) throw new RunningRequesterAlreadyStartedExeption("Requester already started, run cancelation before create new instance.");
            else Runner = Task.Run(() => taskToRunAsync(action, millisecondsdelay));
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
