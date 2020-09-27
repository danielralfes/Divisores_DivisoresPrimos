using MathCalc.Calc;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;
using System.Diagnostics;
using System.Threading.Tasks;

namespace MathCalc.ConsoleCalc
{
    class Program
    {
        static async Task Main(string[] args)
        {
            var services        = new ServiceCollection();
            ConfigureServices(services);
            var serviceProvider = services.BuildServiceProvider();

            var logger = serviceProvider.GetService<ILoggerFactory>();
            var log    = logger.CreateLogger<Program>();

            try
            {
                log.LogInformation("Running...");

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
                log.LogError(ex, "Unhandled Exception");

                if (Debugger.IsAttached)
                    Debugger.Break();
            }
            finally
            {
                //"Press ENTER to exit..."
                Console.ReadLine();
            }
        }


        private static void ConfigureServices(IServiceCollection services)
        {
            services.AddLogging(/*configure => configure.AddConsole()*/) //Para exibir no console descomentar parte comentada
                    .AddSingleton<ICalcPrimeNumber, CalcPrimeNumber>()
                    .Configure<LoggerFilterOptions>(options => options.MinLevel = LogLevel.Information);
        }
    }
}
