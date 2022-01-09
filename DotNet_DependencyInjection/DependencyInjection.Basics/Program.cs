// See https://aka.ms/new-console-template for more information
using DependencyInjection.Basics;
using Microsoft.Extensions.DependencyInjection;
Console.WriteLine("Hello, World!");
DoIt();
Scope();

void DoIt()
{
    IServiceCollection services = new ServiceCollection();

    services.AddTransient<MyService>();

    var serviceProvider = services.BuildServiceProvider();
    var myService = serviceProvider.GetService<MyService>();

    myService.DoIt();
}

void Scope()
{
    IServiceCollection services = new ServiceCollection();

    services.AddTransient<TransientDateOperation>();
    services.AddScoped<ScopedDateOperation>();
    services.AddSingleton<SingletonDateOperation>();

    var serviceProvider = services.BuildServiceProvider();

    Console.WriteLine();
    Console.WriteLine("-------- 1st Request --------");
    Console.WriteLine();

    var transientService = serviceProvider.GetService<TransientDateOperation>();
    var scopedService = serviceProvider.GetService<ScopedDateOperation>();
    var singletonService = serviceProvider.GetService<SingletonDateOperation>();

    Console.WriteLine();
    Console.WriteLine("-------- 2nd Request --------");
    Console.WriteLine();

    var transientService2 = serviceProvider.GetService<TransientDateOperation>();
    var scopedService2 = serviceProvider.GetService<ScopedDateOperation>();
    var singletonService2 = serviceProvider.GetService<SingletonDateOperation>();

    Console.WriteLine();
    Console.WriteLine("-----------------------------");
    Console.WriteLine();
}

