using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RequesterService.Services.RequesterStrategy.ConcreteStrategy
{
    public class ConcreteRequesterLongCompletionExample : IRequestStrategy
    {
        readonly ILogger<ConcreteRequesterLongCompletionExample> logger;

        public Action DoSomeAction()
        {
            return new Action(() => LongCompletionExample());
        }

        private void LongCompletionExample()
        {
            logger.LogInformation("LongCompletion started");
            Task.Delay(30000).Wait();
            logger.LogInformation("LongCompletion finished in ");
        }
    }
}
