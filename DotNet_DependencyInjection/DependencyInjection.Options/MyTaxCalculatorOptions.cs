namespace DependencyInjection.Options
{

    public class MyTaxCalculatorOptions
    {
        public int TaxRatio { get; set; }

        public MyTaxCalculatorOptions()
        {
            TaxRatio = 118;
        }
    }
}
