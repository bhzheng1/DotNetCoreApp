// See https://aka.ms/new-console-template for more information
using DependencyInjection.Options;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

Console.WriteLine("Hello, World!");
ServiceOptionsDemo1();

ServiceOptionsDemo2();

static void ServiceOptionsDemo1()
{
    IServiceCollection services = new ServiceCollection();

    services.AddOptions();
    services.AddTransient<MyTaxCalculator>();

    //There is a pattern that uses custom options classes to represent a group of related settings. 
    services.Configure<MyTaxCalculatorOptions>(options =>
    {
        options.TaxRatio = 135;
    });

    var serviceProvider = services.BuildServiceProvider();
    var calculator = serviceProvider.GetService<MyTaxCalculator>();

    Console.WriteLine(calculator.Calculate(100));
}

static void ServiceOptionsDemo2()
{
    var configuration = new ConfigurationBuilder()
        .AddJsonFile(Path.Combine(Directory.GetCurrentDirectory(), "appsettings.json"))
        .Build();

    IServiceCollection services = new ServiceCollection();

    services.AddOptions();
    services.AddScoped<MyTaxCalculator>();

    services.Configure<MyTaxCalculatorOptions>(configuration.GetSection("TaxOptions"));

    var serviceProvider = services.BuildServiceProvider();

    var calculator = serviceProvider.GetRequiredService<MyTaxCalculator>();
    Console.WriteLine(calculator.Calculate(200));
}