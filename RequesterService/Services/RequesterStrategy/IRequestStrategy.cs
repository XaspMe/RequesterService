using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RequesterService.Services.RequesterStrategy
{
    public interface IRequestStrategy
    {
        public Action DoSomeAction();
    }
}
