// See https://aka.ms/new-console-template for more information
using DependencyInjection.MultipleRegistration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

Console.WriteLine("Hello, World!");
MultipleImplementation();
MultipleImplementationWithTry();
MultipleImplementationWithReplace();

static void MultipleImplementation()
{
    IServiceCollection services = new ServiceCollection();

    services.AddTransient<IHasValue, MyClassWithValue>();
    services.AddTransient<IHasValue, MyClassWithValue2>();

    var serviceProvider = services.BuildServiceProvider();
    var myServices = serviceProvider.GetServices<IHasValue>().ToList();
    var myService = serviceProvider.GetService<IHasValue>();

    Console.WriteLine("----- Multiple Implemantation Services -----------");

    foreach (var service in myServices)
    {
        Console.WriteLine(service.Value);
    }

    Console.WriteLine("----- Multiple Implemantation Service ------------");
    Console.WriteLine(myService.Value);
}

static void MultipleImplementationWithTry()
{
    IServiceCollection services = new ServiceCollection();

    services.AddTransient<IHasValue, MyClassWithValue>();

    //TryAdd- method will not register the service if there is a registered service
    services.TryAddTransient<IHasValue, MyClassWithValue2>();

    var serviceProvider = services.BuildServiceProvider();
    var myServices = serviceProvider.GetServices<IHasValue>().ToList();

    Console.WriteLine("----- Multiple Implemantation Try ----------------");

    foreach (var service in myServices)
    {
        Console.WriteLine(service.Value);
    }
}

static void MultipleImplementationWithReplace()
{
    IServiceCollection services = new ServiceCollection();

    services.AddTransient<IHasValue, MyClassWithValue>();

    //Replace will register service with another one
    services.Replace(ServiceDescriptor.Transient<IHasValue, MyClassWithValue2>());

    var serviceProvider = services.BuildServiceProvider();
    var myServices = serviceProvider.GetServices<IHasValue>().ToList();

    Console.WriteLine("----- Multiple Implemantation Replace ------------");

    foreach (var service in myServices)
    {
        Console.WriteLine(service.Value);
    }

    Console.WriteLine("--------------------------------------------------");
}
