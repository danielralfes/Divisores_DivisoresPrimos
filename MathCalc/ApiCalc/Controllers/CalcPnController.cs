using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Http.Description;
using MathCalc.Calc;
using MathCalc.Calc.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace MathCalc.ApiCalc.Controllers
{
    [ApiController]
    [Route("api/math/primenumber")]
    public class CalcPNController : ControllerBase
    {
        private readonly ILogger<CalcPNController> log;
        private ICalcPrimeNumber calcNumberPrime;

        public CalcPNController(ILogger<CalcPNController> logger, ICalcPrimeNumber calcNumber)
        {
            log             = logger;
            calcNumberPrime = calcNumber;

        }

        /// <summary>
        /// Metódo responsável por calcular todos os divisores que compõem o número e calcular todos os divisores primos que compõem o número.
        /// </summary>
        /// <param name="number">Número inteiro</param>
        /// <returns>Retorna lista de divores e divisores primos</returns>
        [HttpGet]
        [Route("CalculateDividersAndPrimeDividers")]
        [ResponseType(typeof(CalcPrimeNumberModel))]
        [ProducesResponseType(201)]
        [ProducesResponseType(400)]
        [Produces("application/json")]
        [AllowAnonymous] //Apenas para testar no PostMan, retirar para ambiente de produção/testes
        public async Task<IActionResult> Get(long number)
        {
            log.LogInformation($"Número solicitado:{number}");

            try
            {
                var result = await calcNumberPrime.CalculateDividersAndPrimeDividersAsync(number);
                return Ok(result);
            }
            catch (Exception ex)
            {
                log.LogError(ex, "Erro na chamada do 'CalculateDividersAndPrimeDividers'");

                return BadRequest("Erro ao executar chamada");
            }

            
        }
    }
}
