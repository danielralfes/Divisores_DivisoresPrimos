using MathCalc.Calc;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;
using System.Threading.Tasks;

namespace MathCalc.Test
{
    [TestClass]
    public class CalcPrimeTest
    {
        private readonly ICalcPrimeNumber calcNumberPrime;
        private readonly ServiceProvider serviceProvider;

        public CalcPrimeTest()
        {
            var services = new ServiceCollection();

            services.AddLogging()
                    .AddSingleton<ICalcPrimeNumber, CalcPrimeNumber>()
                    .Configure<LoggerFilterOptions>(options => options.MinLevel = LogLevel.Information);

            serviceProvider = services.BuildServiceProvider();
        }

        [TestMethod]
        public void TestMethodIsPrimeDividerAsyncNotPrime()
        {
            var resp =  Task.Run(async () =>
            {
                return await serviceProvider.GetService<ICalcPrimeNumber>().IsPrimeDividerAsync(30);

            }).GetAwaiter().GetResult();

            Assert.IsNotNull(resp);
            Assert.IsFalse(resp);
        }

        [TestMethod]
        public void TestMethodIsPrimeDividerAsync()
        {
            var resp = Task.Run(async () =>
            {
                return await serviceProvider.GetService<ICalcPrimeNumber>().IsPrimeDividerAsync(11);

            }).GetAwaiter().GetResult();

            Assert.IsNotNull(resp);
            Assert.IsTrue(resp);
        }


        [TestMethod]
        public void TestMethodIsCalculateDividersAsync()
        {
            var resp = Task.Run(async () =>
            {
                return await serviceProvider.GetService<ICalcPrimeNumber>().CalculateDividersAsync(60);

            }).GetAwaiter().GetResult();


            Assert.IsNotNull(resp);
            Assert.AreEqual(1, resp.FirstOrDefault());
            Assert.AreEqual(60, resp.LastOrDefault());
        }

        [TestMethod]
        public void TestMethodCalculateDividersAndPrimeDividersAsync()
        {
            var resp = Task.Run(async () =>
            {
                return await serviceProvider.GetService<ICalcPrimeNumber>().CalculateDividersAndPrimeDividersAsync(100);

            }).GetAwaiter().GetResult();


            Assert.IsNotNull(resp.Dividers);
            Assert.IsNotNull(resp.DividersPrime);
            Assert.AreEqual(1, resp.Dividers.FirstOrDefault());
            Assert.AreEqual(100, resp.Dividers.LastOrDefault());
            Assert.AreEqual(2, resp.DividersPrime.FirstOrDefault());
            Assert.AreEqual(5, resp.DividersPrime.LastOrDefault());
        }
    }
}
