using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RequesterService.Exeptions
{
    public class WorkerAlreadyRunException : Exception
    {
        public WorkerAlreadyRunException(string message)
        : base(message)
        {
        }
    }
}
