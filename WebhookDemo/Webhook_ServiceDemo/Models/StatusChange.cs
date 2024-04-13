namespace Webhook_ServiceDemo.Models;

public class StatusChange
{
    public int AgentId { get; set; }
    public Status NewStatus { get; set; }
    public DateTime ChangedTime { get; set; }
}
