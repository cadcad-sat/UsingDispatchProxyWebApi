using System;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using WebApi.Domains;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ValuesController : ControllerBase
    {
        private readonly ILogger<ValuesController> logger;
        private readonly ISampleEvent sample;
        public ValuesController(ILogger<ValuesController> logger, ISampleEvent sample)
        {
            this.logger = logger;
            this.sample = sample;
        }

        // GET api/values
        [HttpGet]
        [ProducesResponseType(200)]
        public ActionResult Get()
        {
            string response;
            try
            {
                response = sample.GetMessage().Result;
            }
            catch (Exception e)
            {
                logger.LogError(e.Message);
                response = e.ToString();
            }

            return Ok(response);
        }
    }
}
