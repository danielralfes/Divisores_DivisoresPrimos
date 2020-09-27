using MathCalc.Calc.Model;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MathCalc.Calc
{
    public class CalcPrimeNumber : ICalcPrimeNumber
    {
        public async Task<CalcPrimeNumberModel> CalculateDividersAndPrimeDividersAsync(long number)
        {
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