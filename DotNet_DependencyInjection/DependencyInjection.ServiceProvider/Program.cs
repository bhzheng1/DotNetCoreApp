// See https://aka.ms/new-console-template for more information
using Microsoft.Extensions.DependencyInjection;

Console.WriteLine("Hello, World!");

FactoryMethodDemo();
InstanceRegistrationDemo();

void FactoryMethodDemo()
{
    IServiceCollection services = new ServiceCollection();

    services.AddTransient<IMyServiceDependency, MyServiceDependency>();
    // Overload method for factory registration
    services.AddTransient(
        provider => new MyService(provider.GetService<IMyServiceDependency>())
    );

    var serviceProvider = services.BuildServiceProvider();
    var instance = serviceProvider.GetService<MyService>();

    instance.DoIt();
}
void InstanceRegistrationDemo()
{
    var instance = new MyInstance { Value = 44 };

    IServiceCollection services = new ServiceCollection();

    services.AddSingleton(instance);

    foreach (ServiceDescriptor service in services)
    {
        if (service.ServiceType == typeof(MyInstance))
        {
            var registeredInstance = (MyInstance)service.ImplementationInstance;

            Console.WriteLine("Registered instance : " + registeredInstance.Value);
        }
    }

    var serviceProvider = services.BuildServiceProvider();
    var myInstance = serviceProvider.GetService<MyInstance>();

    Console.WriteLine("Registered service by instance registration : " + myInstance.Value);
}