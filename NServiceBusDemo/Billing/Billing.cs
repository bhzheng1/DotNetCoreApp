using System;
using System.Threading.Tasks;
using NServiceBus;

namespace Billing
{
    class Billing
    {
        static async Task Main(string[] args)
        {
            Console.Title = "Billing";
            var endpointConfiguration = new EndpointConfiguration("Billing");
            var transport = endpointConfiguration.UseTransport<LearningTransport>();
            var endpointInstance = await Endpoint.Start(endpointConfiguration).ConfigureAwait(false);

            Console.WriteLine("Press Enter to Exit.");
            Console.ReadLine();

            await endpointInstance.Stop().ConfigureAwait(false);
        }
    }
}
