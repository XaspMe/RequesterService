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
        int GetCurrentIterationCount();
    }

    public class BackgroundTasksHostedService : IScopedProcessingService, IHostedService
    {
        int executionCount = 0;
        readonly ILogger<BackgroundTasksHostedService> logger;
        CancellationTokenSource cts;
        CancellationToken ct;
        private Task Worker;

        public string GetServiceStatus()
        {
            return Worker == null ? "Deactivated and suspended" : "Аctivated and launched";
        }

        public int GetCurrentIterationCount()
        {
            return executionCount;
        }

        void IScopedProcessingService.Run(Action action, int millisecondsdelay)
        {
            if (Worker != null) throw new WorkerAlreadyRunException("Worker already started, run cancelation before create new instance.");
            else Worker = Task.Run(() => taskToRunAsync(action, millisecondsdelay));
        }

        private async Task taskToRunAsync(Action action, int millisecondsdelay)
        {
            millisecondsdelay = millisecondsdelay > 1000 ? millisecondsdelay : 1000;


            cts = new CancellationTokenSource();
            ct = cts.Token;
            logger.LogInformation("Worker launched");

            while (!ct.IsCancellationRequested)
            {
                await Task.Factory.StartNew(action, TaskCreationOptions.LongRunning);
                Interlocked.Increment(ref executionCount);
                await Task.Delay(millisecondsdelay);
            }
        }

        void IScopedProcessingService.Stop()
        {
            if (Worker != null)
            {
                cts.Cancel();
                logger.LogInformation("The worker terminated");
                Worker = null;
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
