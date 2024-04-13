
using Webhook_ServiceDemo.Models;

namespace Webhook_ServiceDemo.Services;

public class CallService(AgentService agentService, IHttpClientFactory httpClientFactory, ConsumersService consumersService) : BackgroundService
{
    private readonly Random _random = new Random();
    private readonly HttpClient _httpClient = httpClientFactory.CreateClient();
    private readonly AgentService _agentService = agentService;
    private readonly ConsumersService _consumersService = consumersService;

    /// <summary>
    /// generate agent activities every 3 seconds
    /// trigger the callback endpoint when the status is changed
    /// </summary>
    /// <param name="stoppingToken"></param>
    /// <returns></returns>
    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        while (!stoppingToken.IsCancellationRequested)
        {
            await Task.Delay(3000, stoppingToken);
            var next = _random.Next(1, 4);
            var agent = _agentService.Agents.FirstOrDefault(a => a.Id == next);
            var lastActivityStatus = agent?.Activities.Last()?.Status;
            var newStatus = Status.WaitingCall;

            switch (lastActivityStatus)
            {
                case Status.WaitingCall:
                    newStatus = Status.InACall;
                    agent.Activities.Add(new CallLog { Status = Status.InACall, StatusStartTime = DateTime.UtcNow });
                    break;

                case Status.InACall:
                    newStatus = Status.AfterCall;
                    agent.Activities.Add(new CallLog { Status = Status.AfterCall, StatusStartTime = DateTime.UtcNow });
                    break;
                case Status.AfterCall:
                    agent.Activities.Add(new CallLog { Status = Status.WaitingCall, StatusStartTime = DateTime.UtcNow });
                    break;

                case Status.Break:
                    agent.Activities.Add(new CallLog { Status = Status.WaitingCall, StatusStartTime = DateTime.UtcNow });
                    break;

                case Status.OffWork:
                    agent.Activities.Add(new CallLog { Status = Status.WaitingCall, StatusStartTime = DateTime.UtcNow });
                    break;
            }

            var inCall = new StatusChange
            {
                AgentId = agent.Id,
                NewStatus = newStatus,
                ChangedTime = DateTime.UtcNow
            };
            try
            {
                foreach (var consumerWebhook in _consumersService.ConsumerWebhooks)
                {
                    await _httpClient.PostAsJsonAsync(consumerWebhook, inCall, cancellationToken: stoppingToken);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }
    }
}