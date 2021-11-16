using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using RequesterService.Services;
using RequesterService.Services.RequesterService;
using RequesterService.Services.RequesterStrategy;
using RequesterService.Services.RequesterStrategy.ConcreteStrategy;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RequesterService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RequesterController : ControllerBase
    {
        readonly ILogger<RequesterController> logger;
        readonly IScopedProcessingService scopedProcessingService;
        readonly RequesterStrategyContext requesterStrategyContext;

        public RequesterController(ILogger<RequesterController> logger, 
                                   IScopedProcessingService scopedProcessingService,
                                   RequesterStrategyContext requesterStrategyContext)
        {
            this.requesterStrategyContext = requesterStrategyContext;
            this.scopedProcessingService = scopedProcessingService;
            this.logger = logger;
        }

        [HttpPost("RunGet")]
        public ActionResult CreateGetRequestAndRun([FromQuery] int millisecondsdelay, [FromServices] ConcreteRequesterStrategyGet strategyGet)
        {
            requesterStrategyContext.SetStrategy(strategyGet);
            scopedProcessingService.Run(requesterStrategyContext.DoSomeBusinessLogic(), millisecondsdelay);
            return Ok(new { status = scopedProcessingService.GetServiceStatus() });
        }


        [HttpPost("RunPost")]
        public ActionResult CreatePostRequestAndRun([FromQuery] int millisecondsdelay, [FromServices] ConcreteRequesterStrategyPost strategyPost)
        {
            requesterStrategyContext.SetStrategy(strategyPost);
            scopedProcessingService.Run(requesterStrategyContext.DoSomeBusinessLogic(), millisecondsdelay);
            return Ok(new { status = scopedProcessingService.GetServiceStatus() });
        }

        [HttpPost("RunLongCompletion")]
        public ActionResult CreateLongCompletion([FromQuery] int millisecondsdelay, [FromServices] ConcreteRequesterLongCompletionExample longCompletionExample)
        {
            requesterStrategyContext.SetStrategy(longCompletionExample);
            scopedProcessingService.Run(requesterStrategyContext.DoSomeBusinessLogic(), millisecondsdelay);
            return Ok(new { status = scopedProcessingService.GetServiceStatus() });
        }


        [HttpPost("Cancel")]
        public ActionResult CancelWork()
        {
            scopedProcessingService.Stop();
            return Ok(new { status = scopedProcessingService.GetServiceStatus() });
        }


        [HttpGet("Status")]
        public ActionResult ServiceStatus()
        {
            return Ok(new { status = scopedProcessingService.GetServiceStatus() });
        }
    }
}
