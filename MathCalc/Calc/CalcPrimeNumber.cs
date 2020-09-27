using MathCalc.Calc.Model;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MathCalc.Calc
{
    public class CalcPrimeNumber : ICalcPrimeNumber
    {
        private readonly ILogger log;

        public CalcPrimeNumber(ILogger<CalcPrimeNumber> loggerFactory)
        {
            log = loggerFactory;
        }

        public async Task<CalcPrimeNumberModel> CalculateDividersAndPrimeDividersAsync(long number)
        {
            log.LogInformation("Iniciando execução do método: CalculateDividersAndPrimeDividersAsync");

            var calcPrime = new CalcPrimeNumberModel
            {
                Dividers = await CalculateDividersAsync(number)
            };

            foreach (long numberDivisor in calcPrime.Dividers)
            {
                if (await IsPrimeDividerAsync(numberDivisor))
                {
                    calcPrime.DividersPrime.Add(numberDivisor);
                }
            }

            log.LogInformation("Finalizando execução do método: CalculateDividersAndPrimeDividersAsync");

            return calcPrime;
        }

        public async Task<List<long>> CalculateDividersAsync(long number)
        {
            return await Task.Run(() =>
            {

                var dividers = new List<long>();

                if (number > 0)
                {
                    for (long numberDivisor = 1; numberDivisor <= number; numberDivisor++)
                    {
                        if (number % numberDivisor == 0)
                            dividers.Add(numberDivisor);
                    }
                }
                else if (number < 0)
                {
                    for (long numberDivisor = -1; numberDivisor >= number; numberDivisor--)
                    {
                        if (number % numberDivisor == 0)
                            dividers.Add(numberDivisor);
                    }
                }

                return dividers;

            });
        }
        //Melhorar código acima em desempenho

        public async Task<bool> IsPrimeDividerAsync(long number)
        {
            return await Task.Run(() =>
            {

                if (number > -2 && number < 2)
                    return false;

                if (number > 1)
                {
                    for (long numberDivisor = 2; numberDivisor < number; numberDivisor++)
                    {
                        if (number % numberDivisor == 0)
                            return false;
                    }
                }
                else if (number < -1)
                {
                    for (long numberDivisor = -2; numberDivisor > number; numberDivisor--)
                    {
                        if (number % numberDivisor == 0)
                            return false;
                    }
                }

                return true;
            });
        }
    }
}