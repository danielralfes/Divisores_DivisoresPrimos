using MathCalc.Calc.Model;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MathCalc.Calc
{
    public interface ICalcPrimeNumber
    {
        Task<CalcPrimeNumberModel> CalculateDividersAndPrimeDividersAsync(long number);
        Task<List<long>> CalculateDividersAsync(long number);
        Task<bool> IsPrimeDividerAsync(long number);
    }
}