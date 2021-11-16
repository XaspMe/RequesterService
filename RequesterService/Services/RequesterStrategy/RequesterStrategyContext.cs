using RequesterService.Services.RequesterStrategy;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RequesterService.Services
{
    public class RequesterStrategyContext
    {
        private IRequestStrategy strategy;

        public void SetStrategy(IRequestStrategy strategy)
        {
            this.strategy = strategy;
        }

        public Action DoSomeBusinessLogic()
        {
            return strategy.DoSomeAction();
        }
    }
}

