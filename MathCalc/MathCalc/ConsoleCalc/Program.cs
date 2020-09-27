using MathCalc.Calc;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Diagnostics;
using System.Threading.Tasks;

namespace MathCalc.ConsoleCalc
{
    class Program
    {
        static async Task Main(string[] args)
        {
            try
            {
                //"Running..."

                var services = new ServiceCollection()
                                   .AddSingleton<ICalcPrimeNumber>(new CalcPrimeNumber());

                var serviceProvider = services.BuildServiceProvider();

                Console.WriteLine("Calcula todos os divisores que compõem o número e todos os divisores primos");
                Console.WriteLine("------------------------------------------------------");
                Console.Write("Informe o número: ");
                var number = Convert.ToInt64(Console.ReadLine());
                
                var resultValues = await serviceProvider.GetService<ICalcPrimeNumber>().CalculateDividersAndPrimeDividersAsync(number);

                Console.WriteLine($"Divisores do Número: {number}");
                Console.WriteLine("---------------------");

                foreach (var item in resultValues.Dividers)
                {
                    Console.WriteLine(item);
                }

                Console.WriteLine("---------------------");

                Console.WriteLine($"Divisores Primos do Número: {number}");
                Console.WriteLine("---------------------");

                foreach (var item in resultValues.DividersPrime)
                {
                    Console.WriteLine(item);
                }

                Console.WriteLine("---------------------");


            }
            catch (Exception ex)
            {
                //"Unhandled Exception"

                if (Debugger.IsAttached)
                    Debugger.Break();
            }
            finally
            {
                //"Press ENTER to exit..."
                Console.ReadLine();
            }
        }
    }
}
//TODO:Implementar log
