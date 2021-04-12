using Contracts;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace WebApiCT.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        private readonly ILoggerManager logger;

        public WeatherForecastController(ILoggerManager logger)
        {
            this.logger = logger;
        }

        [HttpGet]
        public IEnumerable<string> Get()
        {
            logger.LogDebug("its debug message");
            return new string[] { "value1", "value2" };
        }
    }
}
