using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RequesterService.Services.RequesterStrategy.ConcreteStrategy
{
    public class ConcreteRequesterStrategyGet : IRequestStrategy
    {
        readonly ILogger<ConcreteRequesterStrategyGet> logger;

        public ConcreteRequesterStrategyGet(ILogger<ConcreteRequesterStrategyGet> logger)
        {
            this.logger = logger;
        }

        public Action ActionForRepeat() => new Action(() => logger.LogInformation("Some GET action"));
    }
}
