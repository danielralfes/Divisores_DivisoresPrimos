using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MathCalc.Calc;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace MathCalc.ApiCalc.Controllers
{
    [ApiController]
    [Route("api/math/primenumber")]
    public class CalcPNController : ControllerBase
    {
        private static readonly string[] Summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        private readonly ILogger<CalcPNController> log;
        private ICalcPrimeNumber calcNumberPrime;

        public CalcPNController(ILogger<CalcPNController> logger, ICalcPrimeNumber calcNumber)
        {
            log             = logger;
            calcNumberPrime = calcNumber;

        }

        [HttpGet]
        [Route("CalculateDividersAndPrimeDividers")]
        public async Task<IActionResult> Get(long number)
        {
            var result = await calcNumberPrime.CalculateDividersAndPrimeDividersAsync(number);

            return Ok(result);
        }
    }
}
