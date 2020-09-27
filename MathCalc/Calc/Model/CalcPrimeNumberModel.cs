using System.Collections.Generic;

namespace MathCalc.Calc.Model
{
    public class CalcPrimeNumberModel
    {
        public CalcPrimeNumberModel()
        {
            Dividers      = new List<long>();
            DividersPrime = new List<long>();
        }

        public List<long> Dividers { get; set; }
        public List<long> DividersPrime { get; set; }
    }
}
