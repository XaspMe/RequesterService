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

        /// <summary>
        /// Создать простой запрос (условный GET)
        /// </summary>
        /// <param name="millisecondsdelay"></param>
        /// <param name="strategyGet"></param>
        /// <returns></returns>
        [HttpPost("RunGet")]
        public ActionResult CreateGetRequestAndRun([FromQuery] int millisecondsdelay, [FromServices] ConcreteRequesterStrategyGet strategyGet)
        {
            requesterStrategyContext.SetStrategy(strategyGet);
            scopedProcessingService.Run(requesterStrategyContext.DoSomeBusinessLogic(), millisecondsdelay);
            return Ok(new { status = scopedProcessingService.GetServiceStatus() });
        }

        /// <summary>
        /// Создать простой запрос (условный POST)
        /// </summary>
        /// <param name="millisecondsdelay"></param>
        /// <param name="strategyPost"></param>
        /// <returns></returns>
        [HttpPost("RunPost")]
        public ActionResult CreatePostRequestAndRun([FromQuery] int millisecondsdelay, [FromServices] ConcreteRequesterStrategyPost strategyPost)
        {
            requesterStrategyContext.SetStrategy(strategyPost);
            scopedProcessingService.Run(requesterStrategyContext.DoSomeBusinessLogic(), millisecondsdelay);
            return Ok(new { status = scopedProcessingService.GetServiceStatus() });
        }

        /// <summary>
        /// Выключение запущенного воркера
        /// </summary>
        /// <returns></returns>
        [HttpPost("Cancel")]
        public ActionResult CancelWork()
        {
            scopedProcessingService.Stop();
            return Ok(new { status = scopedProcessingService.GetServiceStatus() });
        }

        /// <summary>
        /// Статус сервиса
        /// </summary>
        /// <returns></returns>
        [HttpGet("Status")]
        public ActionResult ServiceStatus()
        {
            return Ok(new { status = scopedProcessingService.GetServiceStatus() });
        }
    }
}
