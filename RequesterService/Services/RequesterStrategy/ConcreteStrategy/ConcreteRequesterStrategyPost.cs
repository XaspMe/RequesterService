using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RequesterService.Services.RequesterStrategy.ConcreteStrategy
{
    public class ConcreteRequesterStrategyPost : IRequestStrategy
    {
        readonly ILogger<ConcreteRequesterStrategyPost> logger;

        public ConcreteRequesterStrategyPost(ILogger<ConcreteRequesterStrategyPost> logger)
        {
            this.logger = logger;
        }

        public Action DoSomeAction() => new Action(() => logger.LogInformation("Some POST action"));
    }
}
