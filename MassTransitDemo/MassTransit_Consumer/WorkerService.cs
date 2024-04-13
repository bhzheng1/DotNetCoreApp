using MassTransit;
using MassTransit_Contracts;
using Microsoft.Extensions.Hosting;

namespace MassTransit_Consumer
{
    public class WorkerService : BackgroundService
    {
        readonly IBus _bus;

        public WorkerService(IBus bus)
        {
            _bus = bus;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                await _bus.Publish(new Hello { Value = "test" }, stoppingToken);

                await Task.Delay(1000, stoppingToken);
            }
        }
    }
}