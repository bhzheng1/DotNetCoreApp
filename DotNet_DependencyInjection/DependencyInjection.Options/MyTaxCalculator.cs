using Microsoft.Extensions.Options;

namespace DependencyInjection.Options
{
    public class MyTaxCalculator
    {
        private readonly MyTaxCalculatorOptions _options;

        public MyTaxCalculator(IOptions<MyTaxCalculatorOptions> options)
        {
            _options = options.Value;
        }

        public int Calculate(int amount)
        {
            return amount * _options.TaxRatio / 100;
        }
    }
}
