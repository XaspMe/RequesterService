using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RequesterService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PostRequestExampleController : ControllerBase
    {
        readonly ILogger<PostRequestExampleController> logger;

        public PostRequestExampleController(ILogger<PostRequestExampleController> logger)
        {
            this.logger = logger;
        }

        [HttpPost]
        public ActionResult Post()
        {
            return Ok("Hello from post");
        }
    }
}
