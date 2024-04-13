using Webhook_ConsumerDemo.Models;

namespace Webhook_ConsumerDemo.Services;

public class CallService(AgentService agentService, IHttpClientFactory httpClientFactory) : BackgroundService
{
    private readonly HttpClient _httpClient = httpClientFactory.CreateClient();
    private readonly AgentService _agentService = agentService;

    /// <summary>
    /// register the callback endpoint to the service
    /// </summary>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public override async Task StartAsync(CancellationToken cancellationToken)
    {
        var registration = new EndPointRegistration { Uri = new Uri("http://localhost:5247/api/agentStatus") };
        await _httpClient.PostAsJsonAsync("http://localhost:5243/api/register-endpoint", registration, cancellationToken: cancellationToken);
        await base.StartAsync(cancellationToken);
    }
    /// <summary>
    /// unregister the callback endpoint from the service
    /// </summary>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public override async Task StopAsync(CancellationToken cancellationToken)
    {
        await _httpClient.DeleteAsync("http://localhost:5243/api/unregister-endpoint", cancellationToken: cancellationToken);
        await base.StopAsync(cancellationToken);
    }

    /// <summary>
    /// polling agent status every 2 seconds
    /// </summary>
    /// <param name="stoppingToken"></param>
    /// <returns></returns>
    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        // while (!stoppingToken.IsCancellationRequested)
        // {
        //     await Task.Delay(2000, stoppingToken);

        //     foreach (var agent in _agentService.Agents)
        //     {
        //         var newAgentInfo = await _httpClient.GetFromJsonAsync<Agent>($"http://localhost:5243/api/agents/{agent.Id}", cancellationToken: stoppingToken);
        //         var lastActivity = newAgentInfo?.Activities.Last();
        //         if (agent.Activities.Count != newAgentInfo?.Activities.Count && lastActivity is not null)
        //         {
        //             agent.Activities.Add(lastActivity);
        //         }
        //         Console.WriteLine($"Agent {agent.Id} is in {lastActivity?.Status} status.");
        //     }
        // }
    }
}