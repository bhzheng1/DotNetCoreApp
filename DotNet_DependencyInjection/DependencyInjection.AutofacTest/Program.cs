// See https://aka.ms/new-console-template for more information
using Autofac;
using Autofac.Extensions.DependencyInjection;
using DependencyInjection.AutofacTest;
using Microsoft.Extensions.DependencyInjection;

Console.WriteLine("Hello, World!");
IServiceCollection services = new ServiceCollection();
services.AddTransient<MyService>();

var containerBuilder = new ContainerBuilder();
containerBuilder.RegisterType<MyService>();
containerBuilder.Populate(services);

var container = containerBuilder.Build();
var serviceProvider = new AutofacServiceProvider(container);
var myService = serviceProvider.GetService<MyService>();
myService.DoIt();
