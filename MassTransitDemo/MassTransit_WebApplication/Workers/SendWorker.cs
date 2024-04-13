using MassTransit;
using MassTransit_WebApplication.Contracts;

namespace MassTransit_WebApplication.Workers;

public class SendWorker(IBus bus) : BackgroundService
{
    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        //var endpoint = await bus.GetSendEndpoint(new Uri("queue:hello-world"));
        while (!stoppingToken.IsCancellationRequested)
        {
            await bus.Send(new HelloWorldMessage() { Value = "hello world" }, stoppingToken);
            await Task.Delay(1000, stoppingToken);
        }
    }
}