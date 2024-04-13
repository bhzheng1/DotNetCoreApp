using Webhook_ServiceDemo.Models;

namespace Webhook_ServiceDemo.Services;
public class AgentService
{
    public AgentService()
    {
        Agents = new List<Agent>{
            new Agent{Id = 1},
            new Agent{Id = 2},
            new Agent{Id = 3}
        };
        AddAgentActivities();
    }
    public List<Agent> Agents { get; }
    private void AddAgentActivities()
    {
        Agents.ForEach(a =>
        {
            var callLog = new CallLog { Status = Status.WaitingCall, StatusStartTime = DateTime.UtcNow };
            a.Activities.Add(callLog);
        });
    }
}
