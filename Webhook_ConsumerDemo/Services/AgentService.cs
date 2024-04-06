using Webhook_ConsumerDemo.Models;

namespace Webhook_ConsumerDemo.Services;
public class AgentService
{
    public AgentService()
    {
        Agents = new List<Agent>{
            new Agent{Id = 1},
            new Agent{Id = 2},
            new Agent{Id = 3}
        };
    }
    public List<Agent> Agents { get; }
}