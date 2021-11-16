using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RequesterService.Exeptions
{
    public class RunningRequesterAlreadyStartedExeption : Exception
    {
        public RunningRequesterAlreadyStartedExeption(string message)
        : base(message)
        {
        }
    }
}
