using System;
using System.Threading.Tasks;
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
        private readonly ICalcPrimeNumber calcNumberPrime;

        public CalcPNController(ILogger<CalcPNController> logger, ICalcPrimeNumber calcNumber)
        {
            log             = logger;
            calcNumberPrime = calcNumber;

        }


        /// <summary>
        /// Metódo responsável por calcular todos os divisores que compõem o número e calcular todos os divisores primos que compõem o número.
        /// </summary>
        /// <remarks>
        /// Enviar número inteiro
        /// </remarks>
        /// <param name="number">Número inteiro</param>
        /// <returns>Retorna lista de divisores e divisores primos</returns>
        /// <response code="200">Retorna lista de divisores e divisores primos</response>
        /// <response code="400">Erro ao realizar chamada</response>   
        [HttpGet]
        [Route("listardivisoes/{number}")]
        [ProducesResponseType(typeof(CalcPrimeNumberModel), 200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(500)]
        [Produces("text/json")]
        [AllowAnonymous] //Apenas para testar no Swagger, retirar para ambiente de produção/testes
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
