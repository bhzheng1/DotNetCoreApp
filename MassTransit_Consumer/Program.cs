using Microsoft.Extensions.Hosting;
using MassTransit_Consumer;

// consumer could be in other projects
using MassTransit_Contracts.Consumers;

// consumer could be in current projects
using Consumer = MassTransit_Consumer.Consumers.HelloConsumer;
using Microsoft.Extensions.DependencyInjection;

var builder = Host.CreateApplicationBuilder();

// register a host service to publish messages to the bus
builder.Services.AddHostedService<WorkerService>();

#region InMemoryMassTransit

// register consumers in Consumer project
// only need to pass one type of consumer to let MassTransit find all the consumers in the assembly
//builder.Services.AddInMemoryMassTransit<Consumer>();

// register consumers in Contracts project
//builder.Services.AddInMemoryMassTransit<HelloConsumer>();
#endregion

#region RabbitMQMassTransit
builder.Services.AddRabbitMQMassTransit<Consumer>();
#endregion


var host = builder.Build();
await host.RunAsync();



