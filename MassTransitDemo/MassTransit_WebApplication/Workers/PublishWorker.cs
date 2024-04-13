using MassTransit;
using MassTransit_WebApplication.Contracts;

namespace MassTransit_WebApplication.Workers;

/// <summary>
/// Publish worker will publish message to SNS topic, then message will be transfer to SQS queues that are subscribing the topic
/// </summary>
/// <param name="bus"></param>
public class PublishWorker(IBus bus) : BackgroundService
{
    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        while (!stoppingToken.IsCancellationRequested)
        {
            await bus.Publish(new HelloMessage()
            {
                Value = "world"
            }, stoppingToken);
            await Task.Delay(1000, stoppingToken);
        }
    }
}